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
    private SpriteRenderer _rangeRenderer;
    private SpriteRenderer _selectionSpriteRenderer;
    private SpriteRenderer[] _spriteRenderers;
    
    private void Awake()
    {
        _rangeRenderer = transform.Find("Range").GetComponent<SpriteRenderer>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _selectionSpriteRenderer = transform.Find("Selection Image").GetComponent<SpriteRenderer>();
    }

    public void DeselectSprite()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sortingOrder -= 2;
        }
        
        _rangeRenderer.enabled = false;
        _selectionSpriteRenderer.enabled = false;
    }

    public void SelectSprite()
    {
        foreach (var spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.sortingOrder += 2;
        }
        _rangeRenderer.enabled = true;
        _selectionSpriteRenderer.enabled = true; 
    }
}
