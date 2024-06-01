using System;
using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _selectionSpriteRenderer;
    private SpriteRenderer[] _spriteRenderers;
    
    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _selectionSpriteRenderer = transform.Find("Range").GetComponent<SpriteRenderer>();
    }

    public void DeselectSprite()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sortingOrder -= 2;
        }
        
        _selectionSpriteRenderer.enabled = false;
    }

    public void SelectSprite()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sortingOrder += 2;
        }
        _selectionSpriteRenderer.enabled = true; 
    }

    public void UpgradeRange(Sprite newRange)
    {
        _selectionSpriteRenderer.sprite = newRange;
        _selectionSpriteRenderer.gameObject.GetComponent<CircleCollider2D>().radius = newRange.bounds.extents.x;
    }
}
