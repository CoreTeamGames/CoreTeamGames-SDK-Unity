using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace CoreTeamGamesSDK.SceneLoader
{
    public class AsyncSceneLoader : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool _allowProgressBar, _allowSceneActivation = true;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private float _fadeDuration = 1f;
        private AsyncOperation _operation;
        #endregion

        #region Events
        //Unity Events
        [SerializeField] private UnityEvent _onSceneStartsLoadEvent, _onSceneLoadedEvent;
        [SerializeField] private UnityEvent<float> _onSceneLoadingEvent;

        //C# Events
        public delegate void OnSceneStartsLoad();
        public delegate void OnSceneLoading(float progress);
        public delegate void OnSceneLoaded();

        public OnSceneStartsLoad OnSceneStartsLoadEvent;
        public OnSceneLoading OnSceneLoadingEvent;
        public OnSceneLoaded OnSceneLoadedEvent;
        #endregion

        #region Code

        #region Public Methods
        public void LoadScene(string scene)
        {
            if (scene == "" || SceneManager.GetSceneByName(scene) == null)
                return;

            StartCoroutine(LoadSceneCoroutine(scene));
        }

        public void ActivateLoadedScene()
        {
            if (!_operation.isDone || _operation == null)
                return;

            // _canvasGroup.DOFade(0, _fadeDuration);
            _canvasGroup.blocksRaycasts = false;
            _operation.allowSceneActivation = true;
            OnSceneLoadedEvent?.Invoke();
            _onSceneLoadedEvent?.Invoke();
            StartCoroutine(DestroyObjectAfterTime());
        }
        #endregion

        #region Private Methods
        private IEnumerator LoadSceneCoroutine(string scene)
        {
            DontDestroyOnLoad(gameObject);
            // _canvasGroup.DOFade(1, _fadeDuration);
            yield return new WaitForSeconds(_fadeDuration);

            _canvasGroup.blocksRaycasts = true;

            _operation = SceneManager.LoadSceneAsync(scene);

            OnSceneStartsLoadEvent?.Invoke();
            _onSceneStartsLoadEvent?.Invoke();

            _operation.allowSceneActivation = _allowSceneActivation;

            while (!_operation.isDone)
            {
                float progress = Mathf.Clamp01(_operation.progress / 0.9f);

                OnSceneLoadingEvent?.Invoke(progress);
                _onSceneLoadingEvent?.Invoke(progress);

                if (_allowProgressBar && _progressBar != null && _progressText != null)
                {
                    _progressBar.value = progress;
                    _progressText.text = (progress * 100).ToString("F0") + "%";
                }
                yield return null;
            }

            if (_allowSceneActivation)
            {
                ActivateLoadedScene();
            }
        }

        private IEnumerator DestroyObjectAfterTime()
        {
            yield return new WaitForSeconds(_fadeDuration + 1);
            Destroy(gameObject);
        }
        #endregion
        #endregion
    }
}