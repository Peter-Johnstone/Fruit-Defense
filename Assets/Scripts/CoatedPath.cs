using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using UnityEngine;

public class CoatedPath : MonoBehaviour
{
    private PolygonCollider2D _polygonCollider2D;
    
    private float _timer = 0;

    void Start()
    {
        // Get the BoxCollider component attached to this GameObject
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 10f)
        {
            DeleteSelf();
            return;
        }

        Collider2D[] results = new Collider2D[20];
        
        _polygonCollider2D.OverlapCollider(new ContactFilter2D().NoFilter(), results);
        
        foreach (Collider2D collision in results) 
        {
            if (collision.name != "Enemy(Clone)") return;
                    
            collision.GetComponent<Enemy>().TriggerSlip(); 
        }
    }

    private void DeleteSelf()
    {
        Destroy(gameObject);
    }
}

