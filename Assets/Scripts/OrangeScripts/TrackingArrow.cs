using EnemyScripts;
using UnityEngine;

namespace ArcherScripts
{
    public class TrackingArrow : MonoBehaviour
    {
        [SerializeField] private float arrowSpeed;
        [SerializeField] private Sprite coatedArrow;
        [SerializeField] private GameObject coatedPath;
        
        
        
        private Transform _target;
        private BaseEnemy _baseEnemy;
        private SpriteRenderer _spriteRenderer;
        private Stats _stats;
        
        
        private bool _coated;

    
    
        private void Start()
        {
            _target = GetComponentInParent<TrackingArrowSpawner>().GetTarget();
            if (_target == null)
            {
                
                Destroy(gameObject);
                return;
            }
            _baseEnemy = _target.GetComponent<BaseEnemy>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            TrackingArrowSpawner trackingArrowSpawner= GetComponentInParent<TrackingArrowSpawner>();
            _spriteRenderer.sprite = trackingArrowSpawner.GetAnimationArrowImage();
            _stats = GetComponentInParent<Stats>();
            
            
            _coated = _spriteRenderer.sprite == coatedArrow;
        }

        private void FixedUpdate()
        {
            if (_target == null)
            {
                
                Destroy(gameObject);
                return;
            }

            Vector3 direction = (_target.position - transform.position).normalized;

            float distance = Vector2.Distance(_target.position, transform.position);

            if (distance > arrowSpeed * Time.fixedDeltaTime)
            {
                // Move the arrow normally
                transform.position += direction * (arrowSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if (!_baseEnemy.IsDead)
                {
                    _baseEnemy.LoseLife(1);
                    _stats.IncreaseTotalDamage(1);
                }

                if (_coated)
                    Instantiate(coatedPath, _baseEnemy.transform.position, transform.rotation);
                Destroy(gameObject); 
            }
        }
    }
}
