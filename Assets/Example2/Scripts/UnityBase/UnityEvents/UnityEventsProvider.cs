using System;
using UnityBase.Patterns;

namespace UnityBase.UnityEvents
{
    public class UnityEventsProvider : SingletonMonoBehaviour<UnityEventsProvider>
    {
        public event Action OnNextUpdate;

        private bool _invokeOnNextUpdate;

        private void Update()
        {
            if (OnNextUpdate != null)
            {
                if (_invokeOnNextUpdate)
                {
                    OnNextUpdate?.Invoke();
                    OnNextUpdate = null;
                }
                else
                {
                    _invokeOnNextUpdate = true;
                }
            }
        }
    }
}