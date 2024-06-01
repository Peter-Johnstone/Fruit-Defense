using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    [SerializeField] private Sprite upgradedRange;
    private SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void UpgradeRange()
    {
        _spriteRenderer.sprite = upgradedRange;
    }
}
