using System;
namespace SharpBase.Tasks
{
    public abstract class TaskObject : ITask
    {
        public event Action<ITask> OnUpdate;
        public event Action<ITask> OnComplete;
        public virtual float Progress { get; protected set; }
        public abstract void Start();

        protected void InvokeUpdate()
        {
            OnUpdate?.Invoke(this);
        }

        protected void InvokeComplete()
        {
            Progress = 1.0f;
            OnComplete?.Invoke(this);
            OnComplete = null;
            OnUpdate = null;
        }
    }
}