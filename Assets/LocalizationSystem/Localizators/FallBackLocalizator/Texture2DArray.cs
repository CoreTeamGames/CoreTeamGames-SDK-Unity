using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Texture2D assets array", menuName = "CoreTeamSDK/Localization System/Texture2D assets array")]
public class Texture2DArray : ScriptableObject
{
    [SerializeField] Dictionary<string, Texture2D> _texture2DAssets;

    public Dictionary<string, Texture2D> Texture2DAssets => _texture2DAssets;
}