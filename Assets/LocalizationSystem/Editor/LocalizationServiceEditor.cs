using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

namespace CoreTeamGamesSDK.Localization.Editor
{
    [CustomEditor(typeof(LocalizationService))]
    public class LocalizationServiceEditor : UnityEditor.Editor
    {
        // #region Variables
        // private int _standardLocalizatorSelectedIndex;
        // private int _fallbackLocalizatorSelectedIndex;
        // private Type[] interfaceImplementations;
        // private string[] classNames;
        // private LocalizationService _localizationService;

        // #endregion

        // #region Code
        // private void OnEnable()
        // {
        //     _localizationService = (LocalizationService)target;
        //     var types = typeof(ILocalizator).Assembly.GetTypes();
        //     interfaceImplementations = Assembly.GetAssembly(typeof(ILocalizator))
        //                                    .GetTypes()
        //                                    .Where(t => typeof(ILocalizator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        //                                    .ToArray();


        //     classNames = interfaceImplementations.Select(t => t.FullName).ToArray();

        //     _standardLocalizatorSelectedIndex = _localizationService.StandardLocalizator != null? Array.IndexOf( classNames,_localizationService.StandardLocalizator.GetType().FullName):0;
        //     _fallbackLocalizatorSelectedIndex = _localizationService.StandardLocalizator != null? Array.IndexOf( classNames,_localizationService.FallbackLocalizator.GetType().FullName):0;
        // foreach (var a in interfaceImplementations)
        // {
        //     Debug.Log(a);
        // }
        // }

        // public override void OnInspectorGUI()
        // {
        //     serializedObject.Update();
        //     EditorGUILayout.LabelField("Reader Set-up", EditorStyles.boldLabel);

        //     _standardLocalizatorSelectedIndex = EditorGUILayout.Popup(new GUIContent("Standard Localizator", "The standard localizator for localize game"), _standardLocalizatorSelectedIndex, classNames);
        //     _fallbackLocalizatorSelectedIndex = EditorGUILayout.Popup(new GUIContent("Fallback Localizator", "This localizator uses, when standard localizator doesn\'t found any localizations"), _fallbackLocalizatorSelectedIndex, classNames);

        //     serializedObject.FindProperty("_standardLocalizator").managedReferenceValue = (ILocalizator)Activator.CreateInstance(interfaceImplementations[_standardLocalizatorSelectedIndex]);
        //     serializedObject.FindProperty("_fallbackLocalizator").managedReferenceValue  = (ILocalizator)Activator.CreateInstance(interfaceImplementations[_fallbackLocalizatorSelectedIndex]);
        // }
        // #endregion
    }
}