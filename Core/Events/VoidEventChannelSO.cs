using UnityEngine;
using System.Collections.Generic;

namespace DenizYanar.Events
{
    /// <summary>
    /// This class is used for Events that have no arguments. {Example: Exit game event}
    /// </summary>

    [CreateAssetMenu(menuName = "Slice And Chaos/Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    { 
        private readonly HashSet<VoidEventListenerSO> _listeners = new HashSet<VoidEventListenerSO>();     

        public void Invoke()
        {
            foreach (VoidEventListenerSO listener in _listeners)
                listener.RaiseEvent();
        }

        public void Register(VoidEventListenerSO listener) => _listeners.Add(listener);

        public void Deregister(VoidEventListenerSO listener) => _listeners.Remove(listener);
    }
}
