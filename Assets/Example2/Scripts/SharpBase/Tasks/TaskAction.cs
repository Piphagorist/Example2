using System;

namespace SharpBase.Tasks
{
    public class TaskAction : TaskObject
    {
        private readonly Action _action;

        public TaskAction(Action action)
        {
            _action = action;
        }
        
        public override void Start()
        {
            _action?.Invoke();
            InvokeComplete();
        }
    }
}