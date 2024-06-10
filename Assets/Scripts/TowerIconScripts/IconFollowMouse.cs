using System;
using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UpdateMenuScripts;

public class IconFollowMouse : MonoBehaviour
{

    
    
    private Sprite _canPlaceImage;
    [SerializeField] private Sprite cantPlaceImage;

    [SerializeField] private float hitboxRadius;
    
    private GameObject _towerPrefab;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _draggerImageRenderer;
    private LayerMask _validPlacementLayerMask;
    private LayerMask _towerLayerMask;

    private bool _draggerHiddenThisFrame;
    private Camera _camera;

    private void Start()
    {
        _towerPrefab = GetComponentInParent<TowerIcon>().towerPrefab;
        
        _canPlaceImage = _towerPrefab.transform.Find("Range").GetComponentInChildren<SpriteRenderer>().sprite;
        
        _draggerImageRenderer = transform.Find("Dragger Image").GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _canPlaceImage;
        _camera = Camera.main;
        _draggerImageRenderer.sprite = _towerPrefab.GetComponent<SpriteRenderer>().sprite;
        _validPlacementLayerMask = LayerMask.GetMask("No Tower Terrain") | LayerMask.GetMask("Tower");
    }


    
    private void Update()
    {
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
            UpgradeMenu.Main.TowerWasPlacedThisFramed();
            Economy.Main.Buy(CostsData.Main.GetTowerCost(_towerPrefab.name));
            Instantiate(_towerPrefab, transform.position, transform.rotation);

        }
        else if (validTowerPlacement)
            // Sprite placement is valid, but we aren't clicking. Indicate valid potential placement with blue icon
            _spriteRenderer.sprite = _canPlaceImage;
        else
            // Sprite placement is invalid, and we aren't clicking. Indicate invalid potential placement with red icon
            _spriteRenderer.sprite = cantPlaceImage;
    }

    private void HideDragger()
    {
        TowerMenu.SetDraggerInactive(gameObject);
    }
}
