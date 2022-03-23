using UnityEngine;

namespace DenizYanar.Guns
{
    public class GunLauncher : MonoBehaviour
    {
        [SerializeField] private Transform[] _bulletStartPositions;
        [SerializeField] private Projectile _projectile;

        [SerializeField] private float _projectileSpeed = 1.0f;
        
        public void Shot()
        {

            foreach (var startPos in _bulletStartPositions)
            {
                var right = startPos.right;
                var p = Instantiate(_projectile, startPos.position, Quaternion.Euler(right));
                p.Init(right * _projectileSpeed, 0, lifeTime: 5.0f, author: transform.root.gameObject);
            }
        }
    }
}
 