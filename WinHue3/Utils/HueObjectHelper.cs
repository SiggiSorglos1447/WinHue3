using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media;
using log4net;
using WinHue3.ExtensionMethods;
using WinHue3.Interface;
using WinHue3.Philips_Hue;
using WinHue3.Philips_Hue.BridgeObject;
using WinHue3.Philips_Hue.BridgeObject.BridgeMessages;
using WinHue3.Philips_Hue.BridgeObject.BridgeObjects;
using WinHue3.Philips_Hue.Communication;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Philips_Hue.HueObjects.NewSensorsObject;
using WinHue3.Philips_Hue.HueObjects.ResourceLinkObject;
using WinHue3.Philips_Hue.HueObjects.RuleObject;
using WinHue3.Philips_Hue.HueObjects.SceneObject;
using WinHue3.Philips_Hue.HueObjects.ScheduleObject;

using WinHue3.Settings;
using WinHue3.SupportedLights;
using Action = WinHue3.Philips_Hue.HueObjects.GroupObject.Action;

namespace WinHue3.Utils
{
    public static class HueObjectHelper
    {
        /// <summary>
        /// Logging 
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// CTOR
        /// </summary>
        static HueObjectHelper()
        {
            LightImageLibrary.LoadLightsImages();
        }
/*
        /// <summary>
        /// Get a list of light with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the lights from.</param>
        /// <returns>A List fo lights.</returns>
        public static List<dynamic> GetBridgeLights(Bridge bridge)
        {
            log.Debug($@"Getting all lights from bridge : {bridge.IpAddress}");
            Dictionary<string,dynamic> bresult = bridge?.GetListObjects(HueObjectType.lights);
            if (bresult == null) return null;
            log.Debug("List lights : " + Serializer.SerializeToJson(bresult));
            return ProcessLights(bresult);
        }
        */
/*
        /// <summary>
        /// Get a list of light with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the lights from.</param>
        /// <returns>A List fo lights.</returns>
        public static async Task<List<Light>> GetBridgeLightsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all lights from bridge : {bridge.IpAddress}");
            Dictionary<string, Light> bresult = await bridge?.GetListObjectsAsyncTask<Light>();
            if (bresult == null) return null;
            log.Debug("List lights : " + Serializer.SerializeToJson(bresult));
            return ProcessLights(bresult);
        }*/

        /// <summary>
        /// Get a list of light with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the lights from.</param>
        /// <returns>A List fo lights.</returns>
        public static async Task<List<dynamic>> GetBridgeLightsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all lights from bridge : {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge?.GetListObjectsAsyncTask(HueObjectType.lights);
            if (bresult == null) return null;
            log.Debug("List lights : " + Serializer.SerializeToJson(bresult));
            return ProcessLights(bresult);
        }

        /// <summary>
        /// GEt the list of newly discovered lights
        /// </summary>
        /// <param name="bridge">Bridge to get the new lights from.</param>
        /// <returns>A list of lights.</returns>
        public static async Task<List<dynamic>> GetBridgeNewLightsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting new lights from bridge {bridge.IpAddress}");
            SearchResult bresult = await bridge?.GetNewObjectsAsyncTask<Light>();
            if (bresult == null) return null;
            log.Debug("Search Result : " + bresult.ToString());
            return ProcessSearchResult(bridge, bresult, true);
        }


        /// <summary>
        /// Process the list of lights
        /// </summary>
        /// <param name="listlights">List of lights to process.</param>
        /// <returns>A list of processed lights.</returns>
        private static List<dynamic> ProcessLights(dynamic listlights)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> lights = listlights as IDictionary<string, Object> ?? listlights;
            foreach (KeyValuePair<string, object> kvp in lights)
            {
                dynamic curr = kvp.Value;
                curr.Id = kvp.Key;
                curr.huetype = HueObjectType.lights;
                curr.Image = GetImageForLight(curr.state.reachable ? curr.state.on ? LightImageState.On : LightImageState.Off : LightImageState.Unr, curr.modelid);
                newlist.Add(curr);
            }

            return newlist;
        }

        /// <summary>
        /// Process the search results.
        /// </summary>
        /// <param name="bridge">Bridge to process the search results from</param>
        /// <param name="results">Search result to process</param>
        /// <param name="type">Type of result to process. Lights or Sensors</param>
        /// <returns>A list of objects.</returns>
        private static List<dynamic> ProcessSearchResult(Bridge bridge, SearchResult results,bool type)
        {
            List<dynamic> newlist = new List<dynamic>();
            if (type) // lights
            {
                foreach (KeyValuePair<string, string> kvp in results.listnewobjects)
                {
                    Light bresult = bridge.GetObject<Light>(kvp.Key);
                    if (bresult == null) continue;
                    Light newlight = bresult;
                    newlight.Id = kvp.Key;
                    newlight.Image = GetImageForLight(newlight.state.reachable.GetValueOrDefault() ? newlight.state.on.GetValueOrDefault() ? LightImageState.On : LightImageState.Off : LightImageState.Unr, newlight.modelid);
                    newlist.Add(newlight);
                }
            }
            else // sensors
            {
                foreach (KeyValuePair<string, string> kvp in results.listnewobjects)
                {
                    Sensor bresult = bridge.GetObject<Sensor>(kvp.Key);
                    if (bresult == null) continue;
                    Sensor newSensor = bresult;
                    newSensor.Id = kvp.Key;
                    switch (newSensor.type)
                    {
                        case "ZGPSwitch":
                            log.Debug("New sensor is Hue Tap.");
                            newSensor.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.huetap);
                            break;
                        case "ZLLSwitch":
                            log.Debug("New sensor is dimmer.");
                            newSensor.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.dimmer);
                            break;
                        default:
                            log.Debug("New sensor is generic.");
                            newSensor.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.sensor);
                            break;

                    }
                    newlist.Add(newSensor);
                }
            }

            return newlist;
        }

        /// <summary>
        /// Get a list of group with ID, name and image populated from the selected bridge async.
        /// </summary>
        /// <param name="bridge">Bridge to get the groups from.</param>
        /// <returns>A List of Group.</returns>
        public static List<dynamic> GetBridgeGroups(Bridge bridge)
        {
            log.Debug($@"Getting all groups from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.groups);
            if (bresult == null) return null;
            Dictionary<string, ExpandoObject> gs = bresult;
            dynamic zero = GetGroupZero(bridge);
            if (zero != null)
            {
                gs.Add("0", zero);
            }
            List<dynamic> hr = ProcessGroups(gs);
            log.Debug("List groups : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of group with ID, name and image populated from the selected bridge async.
        /// </summary>
        /// <param name="bridge">Bridge to get the groups from.</param>
        /// <returns>A List of Group.</returns>
        public static async Task<List<dynamic>> GetBridgeGroupsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all groups from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.groups);
            if (bresult == null) return null;
            Dictionary<string, ExpandoObject> gs = bresult;
            dynamic zero = await GetGroupZeroAsynTask(bridge);
            if (zero != null)
            {
                gs.Add("0", zero);
            }
            List<dynamic> hr = ProcessGroups(gs);
            log.Debug("List groups : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Process groups
        /// </summary>
        /// <param name="listgroups">List of group t</param>
        /// <returns>A list of processed group with image and id.</returns>
        private static List<dynamic> ProcessGroups(dynamic listgroups)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> groups = listgroups as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in groups)
            {
                log.Debug("Processing group : " + kvp.Value);
                dynamic curr = kvp.Value;
                curr.huetype = HueObjectType.groups;
                curr.Id = kvp.Key;
                curr.Image = GDIManager.CreateImageSourceFromImage(curr.state.any_on ? (curr.state.all_on ? Properties.Resources.HueGroupOn_Large : Properties.Resources.HueGroupSome_Large) : Properties.Resources.HueGroupOff_Large);
                newlist.Add(kvp.Value);
            }

            return newlist;
        }


        /// <summary>
        /// Get a list of scenes with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the scenes from.</param>
        /// <returns>A List of scenes.</returns>
        public static List<dynamic> GetBridgeScenes(Bridge bridge)
        {
            log.Debug($@"Getting all scenes from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.scenes);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessScenes(bresult);
            log.Debug("List Scenes : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of scenes with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the scenes from.</param>
        /// <returns>A List of scenes.</returns>
        public static async Task<List<dynamic>> GetBridgeScenesAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all scenes from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.scenes);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessScenes(bresult);
            log.Debug("List Scenes : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Process a list of scenes.
        /// </summary>
        /// <param name="listscenes">List of scenes to process.</param>
        /// <returns>A list of processed scenes.</returns>
        private static List<dynamic> ProcessScenes(dynamic listscenes)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> scenes = listscenes as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in scenes)
            {
                dynamic curr = kvp.Value;
                curr.Id = kvp.Key;
                curr.huetype = HueObjectType.scenes;
                log.Debug("Processing scene : " + kvp.Value);
                curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.SceneLarge);
                if (curr.name.Contains("HIDDEN") && !WinHueSettings.settings.ShowHiddenScenes) continue;
                newlist.Add(curr);
            }

            return newlist;
        }

        /// <summary>
        /// Get a list of schedules with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the schedules from.</param>
        /// <returns>A List of schedules.</returns>
        public static List<dynamic> GetBridgeSchedules(Bridge bridge)
        {
            log.Debug($@"Getting all schedules from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.schedules);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessSchedules(bresult);
            log.Debug("List Schedules : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of schedules with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the schedules from.</param>
        /// <returns>A List of schedules.</returns>
        public static async Task<List<dynamic>> GetBridgeSchedulesAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all schedules from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.schedules);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessSchedules(bresult);
            log.Debug("List Schedules : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Process a list of schedules
        /// </summary>
        /// <param name="listschedules">List of schedules to process.</param>
        /// <returns>A list of processed schedules.</returns>
        public static List<dynamic> ProcessSchedules(dynamic listschedules)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> schedules = listschedules as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in schedules)
            {
                log.Debug("Assigning id to schedule ");
                dynamic curr = kvp.Value;
                curr.Id = kvp.Key;
                curr.huetype = HueObjectType.schedules;
                ImageSource imgsource;
                log.Debug("Processing schedule : " + kvp.Value);
                string Time = string.Empty;
                if (curr.localtime == null)
                {
                    log.Debug("LocalTime does not exists try to use Time instead.");
                    if (curr.localtime == null) continue;
                    Time = curr.localtime;
                }
                else
                {
                    log.Debug("Using LocalTime as schedule time.");
                    Time = curr.localtime;
                }

                if (Time.Contains("PT"))
                {
                    log.Debug("Schedule is type Timer.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.timer_clock);
                }
                else if (Time.Contains('W'))
                {
                    log.Debug("Schedule is type Alarm.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.stock_alarm);
                }
                else if (Time.Contains('T'))
                {
                    log.Debug("Schedule is type Schedule.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.SchedulesLarge);
                }
                else
                {
                    log.Debug("Schedule is unknown type.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.schedules);
                }

                curr.Image = imgsource;
                newlist.Add(kvp.Value);
            }

            return newlist;
        }


        /// <summary>
        /// Get a list of rules with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the rules from.</param>
        /// <returns>A List of rules.</returns>
        public static List<dynamic> GetBridgeRules(Bridge bridge)
        {
            log.Debug($@"Getting all rules from bridge {bridge.IpAddress}");
            Dictionary<string,ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.rules);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessRules(bresult);
            log.Debug("List Rules : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of rules with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the rules from.</param>
        /// <returns>A List of rules.</returns>
        public static async Task<List<dynamic>> GetBridgeRulesAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all rules from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.rules);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessRules(bresult);
            log.Debug("List Rules : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Process a list of rules.
        /// </summary>
        /// <param name="listrules">List of rules to process.</param>
        /// <returns>A processed list of rules.</returns>
        private static List<dynamic> ProcessRules(dynamic listrules)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> rules = listrules as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in rules)
            {
                dynamic curr = kvp.Value;
                curr.Id = kvp.Key;
                curr.huetype = HueObjectType.rules;
                log.Debug("Processing rule : " + kvp.Value);
                curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.rules);
                newlist.Add(curr);
            }

            return newlist;
        }

        /// <summary>
        /// Get a list of sensors with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the sensors from.</param>
        /// <returns>A List of sensors.</returns>
        public static List<dynamic> GetBridgeSensors(Bridge bridge)
        {
            log.Debug($@"Getting all sensors from bridge {bridge.IpAddress}");
            Dictionary<string,ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.sensors);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessSensors(bresult);
            log.Debug("List Sensors : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of sensors with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the sensors from.</param>
        /// <returns>A List of sensors.</returns>
        public static async Task<List<dynamic>> GetBridgeSensorsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting all sensors from bridge {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.sensors);
            if (bresult == null) return null;
            List<dynamic> hr = ProcessSensors(bresult);
            log.Debug("List Sensors : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of new sensors with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the sensors from.</param>
        /// <returns>A List of sensors.</returns>
        public static List<dynamic> GetBridgeNewSensors(Bridge bridge)
        {
            log.Debug($@"Getting new sensors from bridge : {bridge.IpAddress}");
            SearchResult bresult = bridge.GetNewObjects<Sensor>();
            List<dynamic> hr = ProcessSearchResult(bridge, bresult, false);
            log.Debug("Search Result : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Get a list of new sensors with ID, name and image populated from the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the sensors from.</param>
        /// <returns>A List of sensors.</returns>
        public static async Task<List<dynamic>> GetBridgeNewSensorsAsyncTask(Bridge bridge)
        {
            log.Debug($@"Getting new sensors from bridge : {bridge.IpAddress}");
            SearchResult bresult = await bridge.GetNewObjectsAsyncTask<Sensor>();
            List<dynamic> hr = ProcessSearchResult(bridge, bresult, false);
            log.Debug("Search Result : " + Serializer.SerializeToJson(hr));
            return hr;
        }

        /// <summary>
        /// Process a list of sensors
        /// </summary>
        /// <param name="listsensors">List of sensors to process.</param>
        /// <returns>A list of processed sensors.</returns>
        private static List<dynamic> ProcessSensors(dynamic listsensors)
        {
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> sensors = listsensors as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in sensors)
            {
                dynamic curr = kvp.Value;
                curr.huetype = HueObjectType.sensors;
                curr.Id = kvp.Key;
                log.Debug("Processing Sensor : " + kvp.Value);
                switch (curr.type)
                {
                    case "ZGPSwitch":
                        log.Debug("Sensor is Hue Tap.");
                        curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.huetap);
                        break;
                    case "ZLLSwitch":
                        log.Debug("Sensor is dimmer.");
                        curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.dimmer);
                        break;
                    case "ZLLPresence":
                        log.Debug("Sensor is Motion.");
                        curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.Motion);
                        break;
                    default:
                        log.Debug("Sensor is generic sensor.");
                        curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.sensor);
                        break;

                }
                newlist.Add(kvp.Value);
            }

            return newlist;
        }

        /// <summary>
        /// Get the list of resource links from the bridge async.
        /// </summary>
        /// <param name="bridge">Bridge to get the resource links from.</param>
        /// <returns>A list of resource links.</returns>
        public static List<dynamic> GetBridgeResourceLinks(Bridge bridge)
        {
            log.Info($@"Fetching Resource links from bridge : {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = bridge.GetListObjects(HueObjectType.resourcelinks);
            if (bresult == null) return null;
            List<dynamic> rl =  ProcessRessourceLinks(bresult);
            return rl;
        }

        /// <summary>
        /// Get the list of resource links from the bridge async.
        /// </summary>
        /// <param name="bridge">Bridge to get the resource links from.</param>
        /// <returns>A list of resource links.</returns>
        public static async Task<List<dynamic>> GetBridgeResourceLinksAsyncTask(Bridge bridge)
        {
            log.Info($@"Fetching Resource links from bridge : {bridge.IpAddress}");
            Dictionary<string, ExpandoObject> bresult = await bridge.GetListObjectsAsyncTask(HueObjectType.resourcelinks);
            if (bresult == null) return null;
            List<dynamic> rl = ProcessRessourceLinks(bresult);
            return rl;
        }

        /// <summary>
        /// Get All Objects from the bridge with ID, name and image populated.
        /// </summary>
        /// <param name="bridge">Bridge to get the datastore from.</param>
        /// <returns>A List of objects.</returns>
        public static List<dynamic> GetBridgeDataStore(Bridge bridge)
        {
            log.Info($@"Fetching DataStore from bridge : {bridge.IpAddress}");
            dynamic bresult = bridge.GetBridgeDataStore();
            List<dynamic> hr = null;
            if (bresult == null) return hr;
            dynamic ds = bresult;
            dynamic zero = GetGroupZero(bridge);
            if (zero != null)
            {
                ds.groups.Add("0",zero);
            }

            hr = ProcessDataStore(ds);
            log.Debug("Bridge data store : " + hr);

            return hr;
        }

        /// <summary>
        /// Get the datastore from the bridge async.
        /// </summary>
        /// <param name="bridge">Bridge to get the datastore from.</param>
        /// <returns>a list of IHueObject</returns>
        public static async Task<List<dynamic>> GetBridgeDataStoreAsyncTask(Bridge bridge)
        {
            log.Info($@"Fetching DataStore from bridge : {bridge.IpAddress}");
            dynamic bresult = await bridge.GetBridgeDataStoreAsyncTask();
            List<dynamic> hr = null;
            if (bresult == null) return hr;
            dynamic ds = bresult;
            dynamic zero = await GetGroupZeroAsynTask(bridge);
            if (zero != null)
            {
                IDictionary<string, object> dic = ds.groups as IDictionary<string,object>;
                dic.Add("0",zero);
                ds.groups = dic as dynamic;
            }

            hr = ProcessDataStore(ds);
            log.Debug("Bridge data store : " + hr);

            return hr;
        }

        /// <summary>
        /// Get the Group Zero.
        /// </summary>
        /// <param name="bridge"></param>
        /// <returns></returns>
        private static dynamic GetGroupZero(Bridge bridge)
        {
            return bridge.GetObject("0", HueObjectType.groups);
        }

        /// <summary>
        /// Get the Group Zero async.
        /// </summary>
        /// <param name="bridge"></param>
        /// <returns></returns>
        private static async Task<dynamic> GetGroupZeroAsynTask(Bridge bridge)
        {
            return (dynamic)await bridge.GetObjectAsyncTask("0", HueObjectType.groups);
        }

        /// <summary>
        /// Process the data from the bridge datastore.
        /// </summary>
        /// <param name="datastore">Datastore to process.</param>
        /// <returns>A list of object processed.</returns>
        private static List<dynamic> ProcessDataStore(dynamic datastore)
        {
            List<dynamic> newlist = new List<dynamic>();
            log.Debug("Processing datastore...");
            newlist.AddRange(ProcessLights(datastore.lights));
            newlist.AddRange(ProcessGroups(datastore.groups));
            newlist.AddRange(ProcessSchedules(datastore.schedules));
            newlist.AddRange(ProcessScenes(datastore.scenes));
            newlist.AddRange(ProcessSensors(datastore.sensors));
            newlist.AddRange(ProcessRules(datastore.rules));
            newlist.AddRange(ProcessRessourceLinks(datastore.resourcelinks));
            log.Debug("Processing complete.");
            return newlist;
        }

        private static List<dynamic> ProcessRessourceLinks(dynamic listrl)
        {
            if(listrl == null) return new List<dynamic>();
            List<dynamic> newlist = new List<dynamic>();
            IDictionary<string, object> rl = listrl as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in rl)
            {
                dynamic curr = kvp.Value;
                curr.huetype = HueObjectType.resourcelinks;
                curr.Id = kvp.Key;
                log.Debug("Processing resource links : " + kvp.Value);
                curr.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.resource);
                newlist.Add(kvp.Value);
            }
            return newlist;
        }

        /// <summary>
        /// Get the mac address of the bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the information from.</param>
        /// <returns>Returns the mac address of the bridge.</returns>
        public static string GetBridgeMac(Bridge bridge)
        {
            BridgeSettings bresult = bridge.GetBridgeSettings();           
            if (bresult == null) return null;
            string hr = string.Empty;
            log.Debug("Fetching bridge mac : " + bresult.mac);
            return bresult.mac;
        }

        /// <summary>
        /// Check if api key is authorized withthe bridge is authorized.
        /// </summary>
        /// <param name="bridge">Bridge to get the information from.</param>
        /// <returns>Check if the bridge is authorized.</returns>
        public static bool IsAuthorized(Bridge bridge)
        {
            BridgeSettings bresult = bridge.GetBridgeSettings();
            if (bresult == null) return false;
            log.Debug("Checking if bridge is authorized : " + Serializer.SerializeToJson(bresult.portalservices));
            return bresult.portalservices != null;
        }

        /// <summary>
        /// Toggle the state of an object on and off (Light or group)
        /// </summary>
        /// <param name="bridge">Bridge to get the information from.</param>
        /// <param name="obj">Object to toggle.</param>
        /// <param name="tt">Transition Time (Optional)</param>
        /// <returns>The new image of the object.</returns>
        public static ImageSource ToggleObjectOnOffState(Bridge bridge, IHueObject obj, ushort? tt = null)
        {
            ImageSource hr = null;
            if (obj is Light)
            {
                Light bresult = bridge.GetObject<Light>(obj.Id);
                if (bresult != null)
                {
                    Light currentState = bresult;

                    if (currentState.state.reachable == false)
                    {
                        hr= GetImageForLight(LightImageState.Unr, currentState.modelid);
                    }
                    else
                    {
                        if (currentState.state.on == true)
                        {
                            log.Debug("Toggling light state : OFF");
                            bool bsetlightstate = bridge.SetState(new State { on = false, transitiontime = tt}, obj.Id);

                            if (bsetlightstate)
                            {
                                hr = GetImageForLight(LightImageState.Off, currentState.modelid);
                            }

                        }
                        else
                        {
                            log.Debug("Toggling light state : ON");
                            bool bsetlightstate = bridge.SetState(new State { on = true, transitiontime = tt, bri = WinHueSettings.settings.DefaultBriLight }, obj.Id);

                            if (bsetlightstate)
                            {

                                hr = GetImageForLight(LightImageState.On, currentState.modelid);
                            }

                        }

                    }
                }


            }
            else
            {
                Group bresult = bridge.GetObject<Group>(obj.Id);

                if (bresult != null)
                {
                    Group currentstate = bresult;
                    if (currentstate.action.on == true)
                    {
                        log.Debug("Toggling group state : ON");
                        bool bsetgroupstate = bridge.SetState(new Action { on = false, transitiontime = tt}, obj.Id);

                        if (bsetgroupstate)
                        {
                            hr = GDIManager.CreateImageSourceFromImage(Properties.Resources.HueGroupOff_Large);
                        }

                    }
                    else
                    {
                        log.Debug("Toggling group state : OFF");
                        bool bsetgroupstate = bridge.SetState(new Action { on = true, transitiontime = tt, bri = WinHueSettings.settings.DefaultBriGroup }, obj.Id);
                        if (bsetgroupstate)
                        {
                            hr = GDIManager.CreateImageSourceFromImage(Properties.Resources.HueGroupOn_Large);
                        }

                    }
                }

            }

            return hr;
        }

        /// <summary>
        /// Toggle the state of an object on and off (Light or group)
        /// </summary>
        /// <param name="bridge">Bridge to get the information from.</param>
        /// <param name="obj">Object to toggle.</param>
        /// <param name="tt">Transition Time (Optional)</param>
        /// <returns>The new image of the object.</returns>
        public static async Task<ImageSource> ToggleObjectOnOffStateAsyncTask(Bridge bridge, dynamic obj, ushort? tt = null)
        {
            ImageSource hr = null;
            if (obj.huetype == HueObjectType.lights)
            {
                dynamic bresult = await bridge.GetObjectAsyncTask(obj.Id, HueObjectType.lights);
                if (bresult == null) return null;
                
                if (bresult.state.reachable == false)
                {
                    hr = GetImageForLight(LightImageState.Unr, bresult.modelid);
                }
                else
                {
                    if (bresult.state.@on == true)
                    {
                        log.Debug("Toggling light state : OFF");
                        bool bsetlightstate = await bridge.SetStateAsyncTask(new State { @on = false, transitiontime = tt }, obj.Id);

                        if (bsetlightstate)
                        {
                            hr = GetImageForLight(LightImageState.Off, bresult.modelid);
                        }

                    }
                    else
                    {
                        log.Debug("Toggling light state : ON");
                        bool bsetlightstate = await bridge.SetStateAsyncTask(new State { @on = true, transitiontime = tt, bri = WinHueSettings.settings.DefaultBriLight }, obj.Id);

                        if (bsetlightstate)
                        {

                            hr = GetImageForLight(LightImageState.On, bresult.modelid);
                        }

                    }

                }
            }
            else
            {
                dynamic bresult = await bridge.GetObjectAsyncTask(obj.Id, HueObjectType.groups);

                if (bresult == null) return null;
                
                if (bresult.action.@on == true)
                {
                    log.Debug("Toggling group state : ON");
                    bool bsetgroupstate = await bridge.SetStateAsyncTask(new Action { @on = false, transitiontime = tt }, obj.Id);

                    if (bsetgroupstate)
                    {
                        hr = GDIManager.CreateImageSourceFromImage(Properties.Resources.HueGroupOff_Large);
                    }

                }
                else
                {
                    log.Debug("Toggling group state : OFF");
                    bool bsetgroupstate = await bridge.SetStateAsyncTask(new Action { @on = true, transitiontime = tt, bri = WinHueSettings.settings.DefaultBriGroup }, obj.Id);
                    if (bsetgroupstate)
                    {
                        hr = GDIManager.CreateImageSourceFromImage(Properties.Resources.HueGroupOn_Large);
                    }

                }
            }

            return hr;
        }

        /// <summary>
        /// Get a list of users on the selected bridge.
        /// </summary>
        /// <param name="bridge">Bridge to get the information from.</param>
        /// <returns>A list of users</returns>
        public static List<Whitelist> GetBridgeUsers(Bridge bridge)
        {
            Dictionary<string,Whitelist> bresult = bridge.GetUserList();
            
            if (bresult == null) return null;
            
            Dictionary<string, Whitelist> brlisteusers = bresult;
            List<Whitelist> listusers = new List<Whitelist>();
            foreach (KeyValuePair<string, Whitelist> kvp in brlisteusers)
            {
                log.Debug($"Parsing user ID {kvp.Key}, {kvp.Value}");
                kvp.Value.Id = kvp.Key;
                listusers.Add(kvp.Value);
            }

            return listusers;
        }

        /// <summary>
        /// List of possible light state.
        /// </summary>
        public enum LightImageState { On = 0, Off = 1, Unr = 3 }

        /// <summary>
        /// Return the new image from the light
        /// </summary>
        /// <param name="imagestate">Requested state of the light.</param>
        /// <param name="modelid">model id of the light.</param>
        /// <returns>New image of the light</returns>
        public static ImageSource GetImageForLight(LightImageState imagestate, string modelid = null)
        {
            string modelID = modelid ?? "default";
            string state = string.Empty;

            switch (imagestate)
            {
                case LightImageState.Off:
                    state = "off";
                    break;
                case LightImageState.On:
                    state = "on";
                    break;
                case LightImageState.Unr:
                    state = "unr";
                    break;
                default:
                    state = "off";
                    break;
            }

            if (modelID == string.Empty)
            {
                log.Debug("STATE : " + state + " empty MODELID using default images");
                return LightImageLibrary.Images["Default"][state];
            }

            ImageSource newImage;

            if (LightImageLibrary.Images.ContainsKey(modelID))
            {
                log.Debug("STATE : " + state + " MODELID : " + modelID);
                newImage = LightImageLibrary.Images[modelID][state];

            }
            else
            {
                log.Debug("STATE : " + state + " unknown MODELID : " + modelID + " using default images.");
                newImage = LightImageLibrary.Images["Default"][state];
            }
            return newImage;
        }


        /// <summary>
        /// Get a specific object from the bridge.
        /// </summary>
        /// <typeparam name="T">Type of object to get</typeparam>
        /// <param name="bridge">the bridge to get the object from</param>
        /// <param name="id">the id of the object</param>
        /// <returns>the requested object or null if error.</returns>
        public static T GetObject<T>(Bridge bridge, string id) where T : IHueObject
        {
            T bresult = bridge.GetObject<T>(id);
            T Object = bresult;
            if (bresult != null)
            {
                if (typeof(T) == typeof(Light))
                {
                    Light light = Object as Light;

                    log.Debug("Light : " + Object);
                    light.Id = id;
                    light.Image =
                        GetImageForLight(
                            light.state.reachable.GetValueOrDefault()
                                ? light.state.on.GetValueOrDefault() ? LightImageState.On : LightImageState.Off
                                : LightImageState.Unr, light.modelid);
                    Object = (T) Convert.ChangeType(light,typeof(T));
                }
                else if (typeof(T) == typeof(Group))
                {
                    Group group = Object as Group;
                    log.Debug("Group : " + group);
                    group.Id = id;
                    group.Image = GDIManager.CreateImageSourceFromImage(group.action.on.GetValueOrDefault() ? Properties.Resources.HueGroupOn_Large : Properties.Resources.HueGroupOff_Large);
                    Object = (T)Convert.ChangeType(group, typeof(T));
                }
                else if (typeof(T) == typeof(Sensor))
                {
                    Sensor sensor = bresult as Sensor;
                    sensor.Id = id;
                    sensor.Image = GDIManager.CreateImageSourceFromImage(sensor.type == "ZGPSwitch" ? Properties.Resources.huetap : Properties.Resources.sensor);
                    Object = (T)Convert.ChangeType(sensor, typeof(T));
                }
                else if (typeof(T) == typeof(Rule))
                {
                    Rule rule = Object as Rule;
                    log.Debug("Rule : " + rule);
                    rule.Id = id;
                    rule.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.rules);
                    Object = (T)Convert.ChangeType(rule, typeof(T));
                }
                else if (typeof(T) == typeof(Scene))
                {
                    Scene scene = Object as Scene;
                    log.Debug("Scene : " + scene);
                    scene.Id = id;
                    scene.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.SceneLarge);
                    Object = (T)Convert.ChangeType(scene, typeof(T));
                }
                else if (typeof(T) == typeof(Schedule))
                {
                    Schedule schedule = Object as Schedule;
                    schedule.Id = id;
                    ImageSource imgsource;
                    if (schedule.localtime.Contains("PT"))
                    {
                        log.Debug("Schedule is type Timer.");
                        imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.timer_clock);
                    }
                    else if (schedule.localtime.Contains('W'))
                    {
                        log.Debug("Schedule is type Alarm.");
                        imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.stock_alarm);
                    }
                    else if (schedule.localtime.Contains('T'))
                    {
                        log.Debug("Schedule is type Schedule.");
                        imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.SchedulesLarge);
                    }
                    else
                    {
                        log.Debug("Schedule is unknown type.");
                        imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.schedules);
                    }

                    schedule.Image = imgsource;
                    Object = (T)Convert.ChangeType(schedule, typeof(T));
                }
                else if (typeof(T) == typeof(Resourcelink))
                {
                    Resourcelink rl = Object as Resourcelink;
                    rl.Id = id;
                    rl.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.resource);
                    Object = (T)Convert.ChangeType(rl, typeof(T));
                }
            }

            return Object;
        }

        /// <summary>
        /// Get object from the bridge in async
        /// </summary>
        /// <param name="bridge">bridge to get the object from.</param>
        /// <param name="id">The id of the object</param>
        /// <param name="objecttype">the type of the object to get.</param>
        /// <returns>the object requested.</returns>
        public static async Task<dynamic> GetObjectAsyncTask(Bridge bridge, string id, HueObjectType objecttype) 
        {
            dynamic bresult = await bridge.GetObjectAsyncTask(id,objecttype);

            if (bresult == null) return null;
            bresult.Id = id;
            
            if (objecttype == HueObjectType.lights)
            {
               
                log.Debug("Light : " + bresult);
                bresult.Id = id;
                bresult.huetype = HueObjectType.lights;
                bresult.Image =
                    GetImageForLight(
                        bresult.state.reachable
                            ? bresult.state.@on ? LightImageState.On : LightImageState.Off
                            : LightImageState.Unr, bresult.modelid);
                
            }
            else if (objecttype == HueObjectType.groups)
            {
                
                bresult.huetype = HueObjectType.groups;
                log.Debug("Group : " + bresult);
                bresult.Id = id;
                bresult.Image = GDIManager.CreateImageSourceFromImage(bresult.action.@on ? Properties.Resources.HueGroupOn_Large : Properties.Resources.HueGroupOff_Large);
                
            }
            else if (objecttype == HueObjectType.sensors)
            {
                
                bresult.Id = id;
                bresult.huetype = HueObjectType.sensors;
                bresult.Image = GDIManager.CreateImageSourceFromImage(bresult.type == "ZGPSwitch" ? Properties.Resources.huetap : Properties.Resources.sensor);
                
            }
            else if (objecttype == HueObjectType.sensors)
            {
                log.Debug("Rule : " + bresult);
                bresult.Id = id;
                bresult.huetype = HueObjectType.sensors;
                bresult.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.rules);

            }
            else if (objecttype == HueObjectType.scenes)
            {
                log.Debug("Scene : " + bresult);
                bresult.Id = id;
                bresult.huetype = HueObjectType.scenes;
                bresult.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.SceneLarge);
            }
            else if (objecttype == HueObjectType.schedules)
            {
                
                bresult.Id = id;
                bresult.huetype = HueObjectType.schedules;
                ImageSource imgsource;
                if (bresult.localtime.Contains("PT"))
                {
                    log.Debug("Schedule is type Timer.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.timer_clock);
                }
                else if (bresult.localtime.Contains("W"))
                {
                    log.Debug("Schedule is type Alarm.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.stock_alarm);
                }
                else if (bresult.localtime.Contains("T"))
                {
                    log.Debug("Schedule is type Schedule.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.SchedulesLarge);
                }
                else
                {
                    log.Debug("Schedule is unknown type.");
                    imgsource = GDIManager.CreateImageSourceFromImage(Properties.Resources.schedules);
                }

                bresult.Image = imgsource;
                
            }
            else if (objecttype == HueObjectType.resourcelinks)
            {
                bresult.Id = id;
                bresult.huetype = HueObjectType.resourcelinks;
                bresult.Image = GDIManager.CreateImageSourceFromImage(Properties.Resources.resource);                    
            }

            return bresult;
        }


    }

}