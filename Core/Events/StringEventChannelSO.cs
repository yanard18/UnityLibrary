using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    
    [CreateAssetMenu(menuName = "Slice And Chaos/Events/String Event Channel")]
    public class StringEventChannelSO : ScriptableObject
    {
        private readonly HashSet<StringEventListener> _listeners = new HashSet<StringEventListener>();

        public void Invoke(string value)
        {
            foreach (StringEventListener listener in _listeners)
                listener.RaiseEvent(value);
        }

        public void Register(StringEventListener listener) => _listeners.Add(listener);

        public void Deregister(StringEventListener listener) => _listeners.Remove(listener);
    }
}
