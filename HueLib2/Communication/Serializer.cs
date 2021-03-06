﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using HueLib2.BridgeMessages;
using Newtonsoft.Json;

namespace HueLib2
{
    /// <summary>
    /// Json Serializer / Deserializer
    /// </summary>
    public static class Serializer
    {
        private static readonly JsonSerializerSettings jss = new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore, StringEscapeHandling = StringEscapeHandling.Default};


        /// <summary>
        /// This method serialize an object into a JSON string,
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON String</returns>
        public static string SerializeToJson<T>(T obj)
        {
            string json = string.Empty;

            try
            {
                if (obj != null)
                    json = JsonConvert.SerializeObject(obj, jss);
            }
            catch (Exception)
            {

            }

            return json;

        }

        /// <summary>
        /// This method deserialize a JSON string into an object
        /// </summary>
        /// <typeparam name="T">Type of object you want the string deserialize into</typeparam>
        /// <param name="json">JSON string to deserialize</param>
        /// <returns>The object result of the deserialized string</returns>


        public static T DeserializeToObject<T>(string json)
        {
            T newObject;
            try
            {

                if (!json.Equals("{}") && !string.IsNullOrEmpty(json))
                {
                    newObject = JsonConvert.DeserializeObject<T>(json, jss);
                }
                else
                {
                    newObject = default(T);
                }

            }
            catch (Exception ex)
            {
                newObject = default(T);
            }
            return newObject;
        }

        public static object DeserializeMessage(string json)
        {
            object obj = new object();


            try
            {
                obj = JsonConvert.DeserializeObject(json);
            }
            catch (Exception)
            {
                obj = null;
            }
            return obj;
        }

        public static SearchResult DeserializeSearchResult(string json)
        {
            SearchResult NewResult = new SearchResult();

            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    NewResult = !json.Equals("{}") ? JsonConvert.DeserializeObject<SearchResult>(json, new SearchResultJsonConverter()) : null;
                }
                else
                {
                    NewResult = null;
                }
            }
            catch (Exception)
            {
                NewResult = null;
            }
            return NewResult;

        }

    }


}
