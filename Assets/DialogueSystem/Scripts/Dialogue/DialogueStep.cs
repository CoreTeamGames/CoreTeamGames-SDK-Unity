using UnityEngine;
using System;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    [Serializable]
    public class DialogueStep
    {
        #region Variables
        [SerializeField] private string _characterNameKey;
        [SerializeField] private string _lineKey;
        [SerializeField] private bool _isLeft;
        [SerializeField] private bool _canSkipOutput = true;
        [SerializeField] private AudioClip _voice;
        [SerializeField] private Sprite _emotion;
        [SerializeField] private bool _overrideOutputSpeed = false;
        [SerializeField] private float _overridenOutputSpeed = 0f;
        #endregion

        #region Properties
        public bool OverrideOutputSpeed => _overrideOutputSpeed;
        public float OverridenOutputSpeed => _overridenOutputSpeed;
        public string CharacterName { get; set; }
        public string Line { get; set; }
        public string CharacterNameKey => _characterNameKey;
        public string LineKey => _lineKey;
        public bool IsLeft => _isLeft;
        public bool CanSkipOutput => _canSkipOutput;
        public AudioClip Voice => _voice;
        public Sprite Emotion => _emotion;
        #endregion
    }
}