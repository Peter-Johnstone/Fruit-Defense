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
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _selectionSpriteRenderer;
    private SpriteRenderer _bowRenderer;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rangeRenderer = transform.Find("Range").GetComponent<SpriteRenderer>();
        _bowRenderer = transform.Find("Bow").GetComponent<SpriteRenderer>();
        _selectionSpriteRenderer = transform.Find("Selection Image").GetComponent<SpriteRenderer>();
    }

    public void DeselectSprite()
    {
        _spriteRenderer.sortingOrder -= 5;
        _bowRenderer.sortingOrder -= 5;
        
        _rangeRenderer.enabled = false;
        _selectionSpriteRenderer.enabled = false;
    }

    public void SelectSprite()
    {
        _spriteRenderer.sortingOrder += 5;
        _bowRenderer.sortingOrder += 5;
        
        _rangeRenderer.enabled = true;
        _selectionSpriteRenderer.enabled = true; 
    }
}
