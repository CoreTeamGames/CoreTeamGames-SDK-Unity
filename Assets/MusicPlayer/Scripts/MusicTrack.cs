using UnityEngine;

namespace CoreTeamGamesSDK.MusicPlayer
{
    [CreateAssetMenu(menuName = "CoreTeamSDK/MusicPlayer/Music Track")]
    public class MusicTrack : ScriptableObject
    {
        #region Variables
        [SerializeField] private string _trackName;
        [SerializeField] private bool _loopTrack = true;

        [SerializeField] private AudioClip _trackIntro;
        [SerializeField] private AudioClip _track;
        [SerializeField] private AudioClip _trackOutro;
        #endregion

        #region Properties
        public string TrackName => _trackName;
        public bool LoopTrack => _loopTrack;

        public AudioClip TrackIntro => _trackIntro;
        public AudioClip Track => _track;
        public AudioClip TrackOutro => _trackOutro;
        #endregion
    }
}