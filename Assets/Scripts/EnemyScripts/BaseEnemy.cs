using System.Collections;
using Scriptable_Objects;
using SingletonScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace EnemyScripts
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyAttributes enemyAttributes; // Stores traits that all enemies have, but change depending on the specific enemy, ie: lifePoints, moveSpeed, etc.
    
        private Animator _animator;
        private Rigidbody2D _rb;
        private Transform _target;
        private SpriteRenderer _spriteRenderer;
        private SpriteLibrary _spriteLibrary;

        public bool IsDead { get; private set; }
        private bool _isSticky;
        private bool _isQueued; // This boolean determines if the enemy has been queued up in the queue that the towers pick their targets from. We only queue when the enemy becomes visible; we don't want shooting off the screen
        
        private string _currentAnimation;
        private int _nextWaypointIndex;
        private float _initialMoveSpeed;
        private int _hp;
        private float _moveSpeed;
        
        
        private void Start()
        {
            _spriteLibrary = GetComponent<SpriteLibrary>();
            _spriteRenderer = gameObject.GetComponent <SpriteRenderer>();
            _animator = gameObject.GetComponent<Animator>();
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _hp = enemyAttributes.lifePoints;
            _target = LevelManager.Main.path[_nextWaypointIndex]; // movement target of the enemy. Refers to the next waypoint.
            
            _moveSpeed = enemyAttributes.moveSpeed;
            _initialMoveSpeed = enemyAttributes.moveSpeed;
        }

        public void LoseLife(int lifeLost)
        {
            _hp -= lifeLost;
            if (_hp <= 0)
            {
                LevelManager.Main.RemoveEnemy(false, gameObject);
            }
        }
        
        private void FixedUpdate()
        {
            if (IsDead)
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
                {
                    // Actually remove the enemy, as it has finished its death animation.
                    Destroy(gameObject);
                }
                return;
            }
            if (!_isQueued && _spriteRenderer.isVisible) // Checks if the enemy has been queued properly.
            {
                LevelManager.Main.AddEnemyToShootableEnemies(gameObject);
                _isQueued = true;
            }
        
            Vector2 direction = (_target.position - transform.position).normalized;
            AnimateMovement(direction);
            Move(direction);
        }

        private void AnimateMovement(Vector2 direction)
        {
            if (IsDead) return;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = (angle + 360) % 360;

            var newAnimation = angle switch
            {
                >= 45 and < 135 => "MoveUp",
                >= 135 and < 225 => "MoveLeft",
                >= 225 and < 315 => "MoveDown",
                _ => "MoveRight"
            };

            if (newAnimation != _currentAnimation)
            {
                _currentAnimation = newAnimation;
                _animator.SetTrigger(_currentAnimation);
            }
        }

    
        private void Move(Vector2 direction)
        {
            _rb.velocity = direction * _moveSpeed;
            if (!(Vector2.Distance(_target.position, transform.position) < 0.1f)) return;
            _nextWaypointIndex++;
            
            if (_nextWaypointIndex == LevelManager.Main.path.Length)
            {
                LevelManager.Main.RemoveEnemy(true, gameObject);
                Destroy(gameObject);
            }
            else
            {
                _target = LevelManager.Main.path[_nextWaypointIndex];
            }
        }

        public void TriggerDeath()
        {
            // triggers the death of the enemy. Doesn't mean we destroy the gameObject because we play the death animation.
            IsDead = true;
            _spriteRenderer.sortingOrder -= 1;
            _rb.velocity = Vector2.zero;
            _animator.SetTrigger("Die");
        }

        public void TriggerStickiness()
        {
            // This function gets called by the coatedPath. All enemies in contact with the coated (sticky) path will get
            // slowed and switch to a sticky animation.
            if (_isSticky) return;
            _isSticky = true;

            _moveSpeed = _initialMoveSpeed/2;
            _spriteLibrary.spriteLibraryAsset = enemyAttributes.stickySprites;
            StartCoroutine(ResetMoveSpeedAfterDelay(2f));
        }
    
        IEnumerator ResetMoveSpeedAfterDelay(float delay)
        {
            // movement speed returns after the given delay
            yield return new WaitForSeconds(delay);

            _isSticky = false;
            _moveSpeed = _initialMoveSpeed; // Reset move speed after delay
            _spriteLibrary.spriteLibraryAsset = enemyAttributes.regularSprites;
        }
    }
}
