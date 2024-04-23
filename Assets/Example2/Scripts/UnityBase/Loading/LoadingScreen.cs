using SharpBase.Tasks;
using UnityEngine;

namespace SG2.Architecture.Loading
{
    public abstract class LoadingScreen : MonoBehaviour
    {
        protected TasksQueue _tasksQueue;
        
        public void SetTasksQueue(TasksQueue tasksQueue)
        {
            _tasksQueue = tasksQueue;

            _tasksQueue.OnUpdate += HandleQueueUpdate;
            _tasksQueue.OnComplete += HandleQueueComplete;
        }

        protected abstract void HandleQueueUpdate();

        private void HandleQueueComplete()
        {
            _tasksQueue.OnUpdate -= HandleQueueUpdate;
            _tasksQueue.OnComplete -= HandleQueueComplete;
        }
    }
}