using UnityEngine;

namespace CoreTeamGamesSDK.SettingsService
{
    public static class SettingsService
    {
        public static SettingsManager Manager
        {
            get
            {
                SettingsManager manager = (SettingsManager)Object.FindObjectOfType(typeof(SettingsManager));
                if (manager == null)
                {
                    Debug.LogError($"Can{"'"}t find SettingsManager");
                    return null;
                }
                return manager;
            }
        }

        public static SettingsValue[] GetSettingsValues()
        {
            if (Manager == null)
                return null;

            SettingsValue[] values = Manager.GetValues();

            if (values == null)
                return null;

            return values;
        }
    }
}