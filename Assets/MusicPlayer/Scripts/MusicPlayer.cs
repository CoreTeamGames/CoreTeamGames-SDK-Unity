using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreTeamGamesSDK.MusicPlayer
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private MusicTrack _currentTrack;
        [SerializeField] private bool _allowSwitchToNextPart = true;
        [SerializeField] private bool _playOnAwake = false;
        private bool _paused = false;
        private float _timeFromTrackStart;
        private ECurrentTrackType _trackType;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            if (_playOnAwake && _currentTrack != null && _source != null)
            Play();
        }
        public void Play()
        {
            if (_source == null)
                return;

            if (_currentTrack == null)
                return;

            _paused = false;

            if (_currentTrack.TrackIntro != null)
            {
                _source.loop = false;

                _allowSwitchToNextPart = true;

                PlayIntroPart(_currentTrack);
            }
            else if (_currentTrack.Track != null)
            {
                PlayLoopPart(_currentTrack);
            }
            else if (_currentTrack.TrackOutro != null)
            {
                _source.loop = false;

                PlayOutroPart(_currentTrack);
            }
            else
            {

            }
        }

        public void Play(MusicTrack track)
        {
            if (_source == null)
                return;

            if (track == null)
                return;

            _currentTrack = track;

            Play();
        }

        public void PauseTrack()
        {
            _source.Pause();
            _paused = true;
        }

        private void Update()
        {
            if (_source == null)
                return;

            if (_paused)
                return;

            if (Time.time - _timeFromTrackStart >= _source.clip.length)
            {
                if (_allowSwitchToNextPart)
                {
                    switch (_trackType)
                    {
                        case ECurrentTrackType.Intro:
                            if (_currentTrack.Track != null)
                                PlayLoopPart(_currentTrack);
                            else if (_currentTrack.TrackOutro)
                                PlayOutroPart(_currentTrack);
                            else
                                PauseTrack();
                            break;

                        case ECurrentTrackType.Loop:
                            if (_currentTrack.LoopTrack)
                                return;

                            if (_currentTrack.TrackOutro)
                                PlayOutroPart(_currentTrack);
                            else
                                PauseTrack();
                            break;

                        case ECurrentTrackType.Outro:
                            PauseTrack();
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void PlayIntroPart(MusicTrack track)
        {
            if (track.TrackIntro != null)
            {
                _source.loop = false;
                _source.clip = track.TrackIntro;
                _source.Play();
                _trackType = ECurrentTrackType.Intro;
                _timeFromTrackStart = Time.time;
            }
        }
        private void PlayLoopPart(MusicTrack track)
        {
            if (track.Track != null)
            {
                _source.loop = track.LoopTrack;
                _source.clip = track.Track;
                _source.Play();
                _trackType = ECurrentTrackType.Loop;
                _timeFromTrackStart = Time.time;
            }
        }
        private void PlayOutroPart(MusicTrack track)
        {
            if (track.TrackOutro != null)
            {
                _source.loop = false;
                _source.clip = track.TrackOutro;
                _source.Play();
                _trackType = ECurrentTrackType.Outro;
                _timeFromTrackStart = Time.time;
            }
        }
    }
}