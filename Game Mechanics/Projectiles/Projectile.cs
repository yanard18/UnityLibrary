using UnityEngine;

namespace DenizYanar.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private bool _hit;

        [SerializeField] private LayerMask _hitBoxLayer;

        [SerializeField] private bool _enableDebug;

        public GameObject Author { get; private set; }

        #region Monobehaviour

        protected virtual void Awake() => _rb = GetComponent<Rigidbody2D>();

        protected virtual void FixedUpdate() => DetectHit();

        #endregion

        #region Private Methods

        private void DetectHit()
        {
            if (_hit) return;

            var velocity = _rb.velocity;
            var currentPosition = _rb.position;
            var desiredVelocityVector = velocity * Time.fixedDeltaTime;

            // ReSharper disable once Unity.PreferNonAllocApi
            RaycastHit2D[] hit = Physics2D.CircleCastAll(
                currentPosition,
                0.1f,
                desiredVelocityVector.normalized,
                desiredVelocityVector.magnitude,
                _hitBoxLayer);


#if UNITY_EDITOR
            if (_enableDebug)
                Debug.DrawRay(currentPosition, desiredVelocityVector, Color.magenta, 5.0f);
#endif

            if (hit.Length <= 0) return;

            foreach (var t in hit)
            {
#if UNITY_EDITOR
                if (_enableDebug)
                    Debug.Log(t.transform.name);
#endif

                if (t.transform.gameObject == Author) continue;

                Hit(t.collider);
                _hit = true;
                transform.position = t.point;

#if UNITY_EDITOR
                if (_enableDebug)
                    Debug.Log("Hit to: " + t.transform.name);
#endif

                return;
            }
        }

        #endregion

        protected void StopProjectile()
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        protected abstract void Hit(Collider2D col);


        #region Public Methods

        public void Init(Vector2 trajectory, float angularVelocity = 0, float lifeTime = 5.0f, GameObject author = null)
        {
            Author = author != null ? author : null;
            _rb.velocity = trajectory;
            _rb.angularVelocity = angularVelocity;
            DetectHit();
            if (lifeTime > 0f)
                Destroy(gameObject, lifeTime);
        }

        #endregion
    }
}