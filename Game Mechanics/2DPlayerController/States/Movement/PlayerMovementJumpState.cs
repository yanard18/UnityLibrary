using System.Collections;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;

namespace DenizYanar.Player
{
    public class PlayerMovementJumpState : State
    {
        private readonly JumpData _jumpData;
        private readonly PlayerMovementController _playerMovementController;
        private readonly SenseEnginePlayer _jumpSense;

        #region Constructor

        public PlayerMovementJumpState(PlayerMovementController playerMovementController, SenseEnginePlayer jumpSense, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _playerMovementController = playerMovementController;
            _jumpData = playerMovementController.JumpDataInstance;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
            _jumpSense = jumpSense;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            Jump();
        }

        #endregion

        #region Local Methods

        private void Jump()
        {
            _jumpData.Rb.velocity = new Vector2(_jumpData.Rb.velocity.x, _jumpData.JumpForce);
            _jumpData.JumpCount--;
            _jumpSense.Play();
            _playerMovementController.StartCoroutine(StartJumpCooldown(0.15f));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            _jumpData.HasCooldown = true;
            yield return new WaitForSeconds(duration);
            _jumpData.HasCooldown = false;
        }

        #endregion
    }
    
    public class JumpData
    {
        private readonly int _maxJumpCount;
        
        public readonly float JumpForce;
        public readonly Rigidbody2D Rb;
        
        public int JumpCount;
        public bool HasCooldown;
        

        public bool CanJump => JumpCount > 0 && HasCooldown == false;

        public void ResetJumpCount() => JumpCount = _maxJumpCount;

        public JumpData(int maxJumpCount, float jumpForce, Rigidbody2D rb)
        {
            _maxJumpCount = maxJumpCount;
            JumpCount = _maxJumpCount;
            JumpForce = jumpForce;
            Rb = rb;
        }

    }
}
