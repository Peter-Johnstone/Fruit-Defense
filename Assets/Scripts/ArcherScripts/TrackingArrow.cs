using UnityEngine;

namespace ArcherScripts
{
    public class TrackingArrow : MonoBehaviour
    {
        [SerializeField] private float arrowSpeed;
        [SerializeField] private Sprite coatedArrow;
        [SerializeField] private GameObject coatedPath;
    
        private Transform _target;
        private Enemy _enemy;
        private SpriteRenderer _spriteRenderer;

        private bool _coated;

    
    
        private void Start()
        {
            _target = GetComponentInParent<TrackingArrowSpawner>().GetTarget();
            if (_target == null)
            {
                
                Destroy(gameObject);
                return;
            }
            _enemy = _target.GetComponent<Enemy>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = GetComponentInParent<TrackingArrowSpawner>().GetAnimationArrowImage();
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
                if (!_enemy.IsDead()) 
                    _enemy.LoseLife(1);
                if (_coated)
                    Instantiate(coatedPath, _enemy.transform.position, transform.rotation);
                Destroy(gameObject); 
            }
        }
    }
}
