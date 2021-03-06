﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace BuildsAppReborn.Infrastructure
{
    [Serializable]
    public class SettingsContainer<TValue> : List<TValue>
    {
        #region Public Static Methods

        public static SettingsContainer<TValue> Load(String fileName)
        {
            if (!File.Exists(fileName))
            {
                return new SettingsContainer<TValue>();
            }

            var json = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<SettingsContainer<TValue>>(json, jsonSerializerSettings);
        }

        #endregion

        #region Public Methods

        public void Save(String fileName)
        {
            var json = JsonConvert.SerializeObject(this, jsonSerializerSettings);

            var directoryPath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(fileName, json, Encoding.UTF8);
        }

        #endregion

        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, PreserveReferencesHandling = PreserveReferencesHandling.All };
    }
}