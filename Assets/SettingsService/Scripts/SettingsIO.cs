using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace CoreTeamGamesSDK.SettingsService
{
    public static class SettingsIO
    {
        public static void WriteSettings(Dictionary<string, object> settingsValues)
        {
            string str = JsonConvert.SerializeObject(settingsValues);
            File.WriteAllText(Application.persistentDataPath + "/Settings.json", str);
            Debug.Log("Saved to: " + Application.persistentDataPath + "/Settings.json");
        }
        public static Dictionary<string, object> ReadSettings()
        {
            string str;
            if (File.Exists(Application.persistentDataPath + "/Settings.json"))
            {
                str = File.ReadAllText(Application.persistentDataPath + "/Settings.json");
                Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
                return values;
            }
            return new Dictionary<string, object>(0);
        }
    }
}