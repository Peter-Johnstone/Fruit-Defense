using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SingletonScripts;
using TowerScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Random = UnityEngine.Random;

public class ArcherShooting : MonoBehaviour
{
    
    [SerializeField] private float circleRadius;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Animator archerAnimator;
    [SerializeField] private Sprite archerLeft;
    [SerializeField] private Sprite archerRight;

    [SerializeField] private SpriteRenderer animationArrowSpriteRenderer;
    [SerializeField] private Sprite coatedArrow;
    [SerializeField] private Sprite regularArrow;
    
    
    [SerializeField] private SpriteLibraryAsset bowLevel2;
    [SerializeField] private SpriteLibraryAsset bowLevel3;
    [SerializeField] private Collider2D rangeCollider;
    
    private const float RotationSpeed = 3;
    private const float PositionSpeed = 10f;
    
    private Upgrades _upgrades;
    private Animator _bowAnimator;
    private Transform _target;
    private SpriteLibrary _spriteLibrary;

    
    private float _timer;
    private bool _isCoating;
    private bool _coatArrowUpgrade;
    private bool _isShooting;
    private float _shootingAnimationDuration;
    

    private void Start()
    {
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _upgrades = GetComponentInParent<Upgrades>();
        _bowAnimator = GetComponent<Animator>();
        
        // Get updates whenever upgrades happen on this tower.
        _upgrades.OnUpgrade += HandleUpgrade;
        LevelManager.Main.OnRoundOver += HandleRoundOver;
        LevelManager.Main.OnRoundStarted += HandleRoundStarted;
        
        
        // Get the duration of the shooting animation
        _shootingAnimationDuration = _bowAnimator.runtimeAnimatorController.animationClips.First(clip => clip.name == "Shoot").length;
        _timer = attackCooldown;
    
    }
    
    private void Update()
    {
        if (LevelManager.Main.enemies.Count == 0) return;
        
        CreateTarget();
        if (_target)
        {
            HandleRotation();
            HandleShooting();
        }
    }

    private void HandleShooting()
    {
        if (_isCoating) return; // We're not shooting if we're coating the arrow.
        
        _timer += Time.deltaTime;
        if (_timer < _shootingAnimationDuration + attackCooldown) return;

        if (Random.value > .8 && !_isCoating && _coatArrowUpgrade)
        {
            // On random chance, we will coat the arrow.
            archerAnimator.SetTrigger("TriggerCoatArrow");
            _isCoating = true;
        }
        else
        {
            TriggerShoot();
        }
    }

    private void HandleRotation()
    {
        // Determine the direction to the target
        Vector2 direction = (_target.position - transform.position).normalized;
        
        // We move the bow along a circular axis around the character sprite
        Vector2 desiredPosition = direction * circleRadius;
        
        transform.localPosition = Vector2.Lerp(transform.localPosition, desiredPosition, PositionSpeed * Time.deltaTime);
        
        // This is the offset because the 0 degrees has bow pointed towards North-West
        const int angularOffset = 90; 
        
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - angularOffset;

        // Create the target rotation
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Smoothly rotate towards the target point
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        archerAnimator.SetBool("FacingRight", Math.Abs(angle + angularOffset) < 90);    
    }


    private void HandleUpgrade()
    {
        if (_upgrades.LeftUpgradePathLevel == 1)
            _coatArrowUpgrade = true;
        if (_upgrades.RightUpgradePathLevel == 1)
        {
            attackCooldown = 1f;   
            _spriteLibrary.spriteLibraryAsset = bowLevel2;
        }

        if (_upgrades.RightUpgradePathLevel == 2)
        {
            attackCooldown = .5f;
            _spriteLibrary.spriteLibraryAsset = bowLevel3;
        }
    }

    private void HandleRoundOver()
    {
        archerAnimator.SetBool("Round Over", true);
    }

    private void HandleRoundStarted()
    {
        archerAnimator.SetBool("Round Over", false);
    }


    public void TriggerShoot()
    {
        if (LevelManager.Main.RoundOver) return;
        
        CreateTarget();
        _bowAnimator.SetTrigger("ShootTrigger");
        _timer = 0;
        
        animationArrowSpriteRenderer.sprite = _isCoating ? coatedArrow : regularArrow;
                
        _isCoating = false;
    }

    private void CreateTarget()
    {
        _target = null;
        
        // Try to find the first target-able enemy in the list of enemies. Break once we find one, alternatively, if none are found, _target remains null.
        foreach (GameObject enemy in LevelManager.Main.enemies)
        {
            if (rangeCollider.IsTouching(enemy.GetComponent<Collider2D>()))
            {
                _target = enemy.transform;
                break;
            }
        }
    }

    public Transform GetTarget()
    {
        return _target;
    }
}
