using System;
using System.Collections.Generic;

namespace SharpBase.Tasks
{
    public class TasksQueue
    {
        public event Action OnUpdate;
        public event Action OnComplete;
        
        public float Progress { get; private set; }
        
        private readonly List<ITask> _tasks = new();
        private int _currentTaskIndex = -1;

        public void AddTask(ITask task)
        {
            _tasks.Add(task);
        }

        public void Start()
        {
            TryToStartNextTask();
        }

        private void TryToStartNextTask()
        {
            if (_currentTaskIndex == _tasks.Count)
            {
                OnComplete?.Invoke();
                return;
            }

            _currentTaskIndex++;
            
            _tasks[_currentTaskIndex].OnUpdate += HandleTaskUpdate;
            _tasks[_currentTaskIndex].OnComplete += HandleTaskComplete;
            _tasks[_currentTaskIndex].Start();
        }

        private void HandleTaskUpdate(ITask task)
        {
            UpdateProgress();
        }

        private void HandleTaskComplete(ITask task)
        {
            _tasks[_currentTaskIndex].OnUpdate -= HandleTaskUpdate;
            _tasks[_currentTaskIndex].OnComplete -= HandleTaskComplete;
            
            TryToStartNextTask();
        }

        private void UpdateProgress()
        {
            Progress = 0;
            
            foreach (ITask task in _tasks)
                Progress += task.Progress;

            Progress /= _tasks.Count;
            
            OnUpdate?.Invoke();
        }
    }
}