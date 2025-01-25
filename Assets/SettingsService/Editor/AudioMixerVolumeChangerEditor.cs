// using UnityEngine;
// using UnityEditor;
// using UnityEditorInternal;
// using UnityEngine.Audio;
// using System.Collections.Generic;

// namespace CoreTeamGamesSDK.SettingsService.Editor
// {
//     [CustomEditor(typeof(AudioMixerVolumeChanger))]
//     public class AudioMixerVolumeChangerEditor : UnityEditor.Editor
//     {
//         private ReorderableList _mixerGroups;
//         private float SingleLineHeight => EditorGUIUtility.singleLineHeight;
//         private AudioMixer _mixer;
//         private SerializedProperty _mixerProperty;
//         private SerializedProperty MixerProperty
//         {
//             get { return _mixerProperty; }
//             set
//             {
//                 _mixerProperty = value;
//                 OnEnable();
//             }
//         }

//         private void OnEnable()
//         {
//             _mixerGroups = new ReorderableList(serializedObject,
//                                 serializedObject.FindProperty("_mixerGroups"),
//                                 false, true, true, true);

//             _mixerProperty = serializedObject.FindProperty("_mixer");
//             _mixer = (AudioMixer)_mixerProperty.objectReferenceValue;

//             string[] _mixerParameters = GetExposedParameters(_mixer);

//             _mixerGroups.elementHeight = SingleLineHeight * 5 + 4;
//             _mixerGroups.drawHeaderCallback =
//             (Rect rect) =>
//             {
//                 EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), $"Settings Values With AudioMixer Exposed Values");
//             };
//             _mixerGroups.drawElementCallback =
//             (Rect rect, int index, bool isActive, bool isFocused) =>
//             {
//                 rect.y += 2;
//                 var _element = _mixerGroups.serializedProperty.GetArrayElementAtIndex(index);
//                 var _settingsValue = _element.FindPropertyRelative("_settingsValue");
//                 var _mixerExposedValue = _element.FindPropertyRelative("_mixerExposedValue");
//                 int _exposedValueIndex = GetArrayIndexByElement(_mixerExposedValue.stringValue);
//                 _mixerExposedValue.stringValue = _mixerParameters[EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), "Mixer Exposed Value", _exposedValueIndex, _mixerParameters)];
//             };
//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             EditorGUILayout.PropertyField(MixerProperty, new GUIContent("AudioMixer"));
//             if (MixerProperty.objectReferenceValue != null)
//             {
//                 _mixerGroups.DoLayoutList();
//             }

//             serializedObject.ApplyModifiedProperties();
//         }

//         private string[] GetExposedParameters(AudioMixer mixer)
//         {
//             List<string> exposedParams = new List<string>();

//             // Using reflection to access the AudioMixer's ExposedParameters
//             var dynMixer = new SerializedObject(mixer);
//             var parameters = dynMixer.FindProperty("m_ExposedParameters");

//             if (parameters != null && parameters.isArray)
//             {
//                 for (int i = 0; i < parameters.arraySize; i++)
//                 {
//                     var param = parameters.GetArrayElementAtIndex(i);
//                     var nameProp = param.FindPropertyRelative("name");
//                     if (nameProp != null)
//                     {
//                         exposedParams.Add(nameProp.stringValue);
//                     }
//                 }
//             }

//             return exposedParams.ToArray();
//         }

//         private int GetArrayIndexByElement(string element)
//         {
//             string[] _params = GetExposedParameters(_mixer);
//             for (int i = 0; i < _params.Length; i++)
//             {
//                 if (element == _params[i])
//                     return i;
//             }
//             return 0;
//         }
//     }
// }