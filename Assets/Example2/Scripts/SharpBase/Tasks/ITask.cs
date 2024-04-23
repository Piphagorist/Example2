using System;

namespace SharpBase.Tasks
{
    public interface ITask
    {
        event Action<ITask> OnUpdate;
        event Action<ITask> OnComplete;

        float Progress { get; }

        void Start();
    }
}