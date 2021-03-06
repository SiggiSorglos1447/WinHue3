﻿using WinHue3.Philips_Hue.HueObjects.GroupObject;
using WinHue3.Philips_Hue.HueObjects.LightObject;
using WinHue3.Philips_Hue.HueObjects.ResourceLinkObject;
using WinHue3.Philips_Hue.HueObjects.RuleObject;
using WinHue3.Philips_Hue.HueObjects.SceneObject;
using WinHue3.Philips_Hue.HueObjects.ScheduleObject;
using WinHue3.Philips_Hue.HueObjects.SensorObject;

namespace WinHue3.Philips_Hue.HueObjects.Common
{
    public static class HueObjectCreator 
    {
        public static IHueObject CreateHueObject(string huetype)
        {
            switch (huetype)
            {
                case "lights":
                case "light":
                    return new Light();
                case "groups":
                case "group":
                    return new Group();
                case "schedules":
                case "schedule":
                    return new Schedule();
                case "rules":
                case "rule":
                    return new Rule();
                case "resourcelinks":
                case "resourcelink":
                    return new Resourcelink();
                case "sensors":
                case "sensor":
                    return SensorFactory.CreateSensor(huetype);
                case "scenes":
                case "scene":
                    return new Scene();
                default:
                    return null;
            }
        }
    }
}
