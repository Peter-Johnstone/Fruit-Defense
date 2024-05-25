using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
    
    private Transform _target;
    private Enemy _enemy;
    private SpriteRenderer _spriteRenderer;
    
    
    private void Start()
    {
        _target = GetComponentInParent<TrackingArrowSpawner>().GetTarget();
        _enemy = _target.GetComponent<Enemy>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = GetComponentInParent<TrackingArrowSpawner>().GetAnimationArrowImage();
    }

    private void FixedUpdate()
    {
        if (!_target)
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
            Destroy(gameObject); 
        }
    }
}
