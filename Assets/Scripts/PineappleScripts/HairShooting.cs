using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using UnityEngine;

public class HairShooting : MonoBehaviour
{

    [SerializeField] private float follicleMoveSpeed;

    [SerializeField] private Collider2D rangeCollider;

    [SerializeField] private float shootSpeedCooldown;

    [SerializeField] private GameObject hairFollicleAttackPrefab;
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
            Instantiate(hairFollicleAttackPrefab, this.transform);
            _timer = 0;
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
}
