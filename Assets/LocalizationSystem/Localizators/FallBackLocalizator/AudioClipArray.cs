using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio assets array", menuName = "CoreTeamSDK/Localization System/Audio assets array")]
public class AudioClipArray : ScriptableObject
{
    [SerializeField] Dictionary<string, AudioClip> _audioAssets;

    public Dictionary<string, AudioClip> AudioAssets => _audioAssets;
}