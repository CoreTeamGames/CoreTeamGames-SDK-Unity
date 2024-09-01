using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Text assets array",menuName = "CoreTeamSDK/Localization System/Text assets array")]
public class TextAssetArray : ScriptableObject
{
    [SerializeField] Dictionary<string,TextAsset> _textAssets;

    public Dictionary<string,TextAsset> TextAssets => _textAssets;
}