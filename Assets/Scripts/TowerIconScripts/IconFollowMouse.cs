using System;
using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class IconFollowMouse : MonoBehaviour
{

    
    
    [SerializeField] private Sprite canPlaceImage;
    [SerializeField] private Sprite cantPlaceImage;

    [SerializeField] private float hitboxRadius;

    public static bool TowerPlacedThisFrame;
    private GameObject _towerPrefab;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _draggerImageRenderer;
    private bool _currentlyShowing = false;
    private LayerMask _validPlacementLayerMask;
    private LayerMask _towerLayerMask;
    
    private Camera _camera;
    
    private void OnEnable()
    {
        TowerIcon.OnClicked += HandleOnClicked;
    }

    private void OnDisable()
    {
        TowerIcon.OnClicked -= HandleOnClicked;
    }

    private void HandleOnClicked()
    {
        if (!_currentlyShowing)
        {
            _currentlyShowing = true;
            _spriteRenderer.enabled = true;
            _draggerImageRenderer.enabled = true;
        }
    }

    private void Start()
    {
        _towerPrefab = GetComponentInParent<TowerIcon>().towerPrefab;
        _draggerImageRenderer = transform.Find("Dragger Image").GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = canPlaceImage;
        _camera = Camera.main;
        _draggerImageRenderer.sprite = _towerPrefab.GetComponent<SpriteRenderer>().sprite;
        
        _validPlacementLayerMask = LayerMask.GetMask("No Tower Terrain") | LayerMask.GetMask("Tower");
    }


    
    private void Update()
    {
        TowerPlacedThisFrame = false;
        if (!_currentlyShowing) return;
        
        
        Vector3 cameraPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, 0);
        
        bool validTowerPlacement = !Physics2D.OverlapCircle(transform.position, hitboxRadius, _validPlacementLayerMask) &&
                                   Economy.Main.CheckCanBuy(CostsData.Main.GetTowerCost(_towerPrefab.name));

        if (Input.GetMouseButtonDown(0))
        {
            // We've clicked screen: Stop dragging sprite
            HideDragger();

            if (!validTowerPlacement) return;
            // Valid Placement + Click: Buy and Place Tower
            TowerPlacedThisFrame = true;
            Economy.Main.BuyTower(CostsData.Main.GetTowerCost(_towerPrefab.name));
            Instantiate(_towerPrefab, transform.position, transform.rotation);

        }
        else if (validTowerPlacement)
            // Sprite placement is valid, but we aren't clicking. Indicate valid potential placement with blue icon
            _spriteRenderer.sprite = canPlaceImage;
        else
            // Sprite placement is invalid, and we aren't clicking. Indicate invalid potential placement with red icon
            _spriteRenderer.sprite = cantPlaceImage;
    }

    private void HideDragger()
    {
        _spriteRenderer.enabled = false;
        _draggerImageRenderer.enabled = false;
        _currentlyShowing = false;
    }
}
