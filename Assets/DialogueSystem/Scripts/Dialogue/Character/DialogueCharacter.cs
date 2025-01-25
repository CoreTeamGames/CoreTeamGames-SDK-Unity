using UnityEngine;

[CreateAssetMenu(menuName = "CoreTeamSDK/Dialogue System/Character")]
public class DialogueCharacter : ScriptableObject
{
    [SerializeField] private string _characterNameKey;
    [SerializeField] private Sprite[] _portraits;
    [SerializeField] private AudioClip[] _voices;

    public string CharacterNameKey => _characterNameKey;
    public Sprite[] Portraits => _portraits;
    public AudioClip[] Voices => _voices;
}