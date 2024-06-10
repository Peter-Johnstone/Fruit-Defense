using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using UnityEngine;

public class HairShooting : MonoBehaviour
{

    [SerializeField] private float follicleMoveSpeed;

    [SerializeField] private Collider2D rangeCollider;

    [SerializeField] private float shootSpeedCooldown;
    [SerializeField] private Animator pineappleHairAnimator;

    [SerializeField] private GameObject hairFollicleAttackPrefab;
    [SerializeField] private GameObject hairFollicleAttackPrefabHot;
    private float _timer;
    private LayerMask _enemyLayerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        
        if (_timer > shootSpeedCooldown && rangeCollider.IsTouchingLayers(_enemyLayerMask))
        {
            // Start Attack Animation
            pineappleHairAnimator.SetTrigger("TriggerAttack");
            
            // Some arbitrary number, we reset the timer properly after we fire the projectiles.
            _timer = -10000f;
        }
    }

    public float GetFollicleAttackSpeed()
    {
        return follicleMoveSpeed;
    }

    public Collider2D GetRangeCollider()
    {
        return rangeCollider;
    }

    public void FireProjectiles()
    {
        // Fire the projectiles
        Instantiate(hairFollicleAttackPrefab, this.transform);
        _timer = 0;
    }

    public void FireProjectilesHot()
    {
        Instantiate(hairFollicleAttackPrefabHot, this.transform);
        _timer = 0;
    }
}
