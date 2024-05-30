using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;

public class FollicleAttack : MonoBehaviour
{

    [SerializeField] private float directionDegrees;
    private Vector3 _directionVector;
    private float _moveSpeed;
    private Collider2D _rangeCollider;
    private Stats _stats;
    
    // Start is called before the first frame update
    void Start()
    {
        float radians = directionDegrees * Mathf.Deg2Rad;
        _directionVector = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
        _moveSpeed = GetComponentInParent<HairShooting>().GetFollicleAttackSpeed();
        _rangeCollider = GetComponentInParent<HairShooting>().GetRangeCollider();
        _stats = GetComponentInParent<Stats>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += _directionVector * (_moveSpeed * Time.fixedDeltaTime);
        if (!_rangeCollider.bounds.Contains(transform.position))
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy" && !other.GetComponent<BaseEnemy>().IsDead)
        {
            other.GetComponent<BaseEnemy>().LoseLife(1);
            _stats.IncreaseTotalDamage(1);
            Destroy(gameObject);
        }
    }
}
