using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
    [SenseEnginePath("GameObjects/Instantiate Corpse")]
    public class SenseInstantiateCorpse : Sense
    {
        [SerializeField] private GameObject[] _corpseGameObjects;
        
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _explosionForce = 10.0f;
        [SerializeField] private float _spawnRadius = 1.0f;
        
        private void Awake()
        {
            Label = "Instantiate Corpse";
        }

        public override void Play()
        {
            foreach (var t in _corpseGameObjects)
            {
                var pos  = _spawnPosition.position;
                Vector2 randomPos = Random.insideUnitSphere * _spawnRadius + pos;
                
                var obj = Instantiate(t, randomPos, Quaternion.identity);
                var r = obj.GetComponent<Rigidbody2D>();
                var dir = randomPos - (Vector2)pos;
                dir.Normalize();
                r.AddForce(dir * _explosionForce, ForceMode2D.Impulse);
            }
        }
    }
}
