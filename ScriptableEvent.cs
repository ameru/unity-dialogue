using System;
using UnityEngine.Events;

namespace Helpers
{
    [Serializable]
    public class ScriptableEvent
    {
        public string eventName;
        public UnityEvent unityEvent;
    }
}
