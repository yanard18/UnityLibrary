using UnityEngine;

namespace DenizYanar
{
    public class PlayerMagnetInput : MonoBehaviour
    {
        [SerializeField] private PlayerInputs _inputs;

        private MagnetController _magnetController;


        public void SetMagnetController(MagnetController m) => _magnetController = m;

        private void OnEnable()
        {
            _inputs.OnTelekinesisStarted += OnMagnetInputPressed;
            _inputs.OnTelekinesisCancelled += OnMagnetInputReleased;
        }
        
        private void OnDisable()
        {
            _inputs.OnTelekinesisStarted -= OnMagnetInputPressed;
            _inputs.OnTelekinesisCancelled -= OnMagnetInputReleased;
        }

        private void OnMagnetInputPressed()
        {
            if(_magnetController == null) return;
            _magnetController.ActivateMagnet(true);
        }

        private void OnMagnetInputReleased()
        {
            if(_magnetController == null) return;
            _magnetController.ImpulseMagnet(EMagnetPolar.PUSH, 1000f, 0.05f);
        }
    }
}
