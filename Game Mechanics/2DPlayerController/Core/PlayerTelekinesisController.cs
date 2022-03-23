using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{
    public class PlayerTelekinesisController : MonoBehaviour
    {
        private bool _isEnable;
       
        [SerializeField] private PlayerInputs _inputs;
        [SerializeField] private float _smoothTime = 0.4f;
        
        public TelekinesisObject MarkedObject;

        #region Monobehaviour

        private void FixedUpdate()
        {
            if(Camera.main is null) return;
            if(MarkedObject is null) return;
            if(_isEnable is false) return;


            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            MarkedObject.Force(mousePos, _smoothTime);
        }

        private void OnEnable()
        {
            _inputs.OnTelekinesisStarted += OnTelekinesisStarted;
            _inputs.OnTelekinesisCancelled += OnTelekinesisCancelled;
        }

        private void OnDisable()
        {
            _inputs.OnTelekinesisStarted -= OnTelekinesisStarted;
            _inputs.OnTelekinesisCancelled -= OnTelekinesisCancelled;
        }

        #endregion

        private void OnTelekinesisStarted()
        {
            _isEnable = true;
        }

        private void OnTelekinesisCancelled()
        {
            _isEnable = false;
            if(MarkedObject != null)
                MarkedObject.OnRelease();
        }

        public void ReleaseTelekinesis()
        {
            OnTelekinesisCancelled();
            MarkedObject = null;
        }
    }
}
