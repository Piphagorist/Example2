using System.Collections;
using SharpBase.Tasks;
using UnityBase.UnityEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBase.Tasks
{
    public class TaskSceneLoading : TaskObject
    {
        private readonly string _sceneName;
        private readonly bool _additiveMode;

        public TaskSceneLoading(string sceneName, bool additive)
        {
            _sceneName = sceneName;
            _additiveMode = additive;
        }

        public override void Start()
        {
            UnityEventsProvider.Instance.StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            LoadSceneMode mode = _additiveMode ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName, mode);

            while (!asyncOperation.isDone)
            {
                Progress = asyncOperation.progress;
                InvokeUpdate();
                yield return null;
            }

            InvokeComplete();
        }
    }
}