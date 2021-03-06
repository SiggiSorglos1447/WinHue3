﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HueLib2.BridgeMessages
{
    public class MessageJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            
            JObject jsonObject = JObject.Load(reader);
            List<JProperty> props = jsonObject.Properties().ToList();

            foreach (JProperty p in props)
            {
                switch (p.Name)
                {
                    case "success":
                        Success suc = new Success();
                        Dictionary<string, dynamic> ko = jsonObject.ToObject<Dictionary<string, dynamic>>();
                        foreach (KeyValuePair<string, dynamic> k in ko)
                        {
                            if (k.Value is string)
                            {
                                suc.Address = k.Value;
                            }
                            else
                            {
                                Dictionary<string, dynamic> d = ((JObject)k.Value).ToObject<Dictionary<string, dynamic>>();
                                foreach (dynamic entry in d)
                                {
                                    suc.Address = entry.Key;
                                    suc.value = entry.Value.ToString();
                                }
                            }
                        }
                        return suc;
                    case "error":
                        Dictionary<string, string> err = p.Value.ToObject<Dictionary<string, string>>();
                        Error e = new Error();
                        e.type = Convert.ToInt32(err["type"]);
                        e.address = err["address"];
                        e.description = err["description"];
                        return e;
                    default:
                        throw new NotSupportedException();
                
                }
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IMessage);
        }
    }
}
