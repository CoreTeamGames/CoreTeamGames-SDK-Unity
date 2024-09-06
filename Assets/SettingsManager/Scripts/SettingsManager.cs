using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
   [SerializeField] private bool _allowAutoLoadSettings = true;
   [SerializeField] SettingsValues _settingsValuesBaseList;
   [SerializeField] SettingsValue[] _settingsValues;

   public SettingsValues SettingsValuesBaseList => _settingsValuesBaseList;

   private void Start() 
   {
      SettingsIO.WriteSettings(_settingsValues);
   }
}
