using SG2.Base.Extensions;
using TMPro;
using UnityEngine;

namespace SG2.Architecture.Loading
{
    public class LoadingScreenText : LoadingScreen
    {
        [SerializeField] private TMP_Text _progressText;
        
        protected override void HandleQueueUpdate()
        {
            _progressText.text = _tasksQueue.Progress.GetPercents();
        }
    }
}