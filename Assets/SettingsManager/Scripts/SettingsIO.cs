using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class SettingsIO
{
    public static void WriteSettings(SettingsValue[] settingsValues)
    {
        string str = JsonConvert.SerializeObject(settingsValues,Formatting.Indented);
        Debug.Log(str);
    }
    public static List<SettingsValue> ReadSettings()
    {
return null;
    }
}
