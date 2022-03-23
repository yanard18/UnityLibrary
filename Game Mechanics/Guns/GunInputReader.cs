using UnityEngine;
using System;

namespace DenizYanar.Guns
{
    public abstract class GunInputReader : MonoBehaviour
    {
        public event Action OnFireStarted;
        public event Action OnFireCancelled;
        public event Action OnReload;

        public void InvokeOnFireStarted() => OnFireStarted?.Invoke();
        public void InvokeOnFireCancelled() => OnFireCancelled?.Invoke();
        public void InvokeOnReload() => OnReload?.Invoke();
    }
}
