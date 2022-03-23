using DenizYanar.FSM;
using DenizYanar.Inputs;
using UnityEngine;

namespace DenizYanar.Player
{
    public class PlayerMovementSliceState : State
    {

        private readonly Rigidbody2D _rb;
        private readonly PlayerInputs _inputs;
        private readonly float _teleportDistance;
        private readonly float _speedReductionAfterTeleport;
        
        public bool HasFinished;

        #region Constructor

        public PlayerMovementSliceState(Rigidbody2D rb, PlayerSettings settings, PlayerInputs inputs)
        {
            _rb = rb;
            _inputs = inputs;
            _teleportDistance = settings.SliceTeleportDistance;
            _speedReductionAfterTeleport = settings.SliceSpeedReductionAfterTeleport;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            var direction = _rb.velocity.normalized;
            _rb.MovePosition(_rb.position + direction * _teleportDistance);
            HasFinished = true;
        }

        public override void OnExit()
        {
            _rb.velocity /= _speedReductionAfterTeleport;
            HasFinished = false;
        }

        #endregion
        
        
    }
}