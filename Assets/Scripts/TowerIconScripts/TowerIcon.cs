using System;
using System.Collections;
using System.Collections.Generic;
using SingletonScripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerIcon : MonoBehaviour
{

    public GameObject towerPrefab; 
    private Collider2D _collider2D;

    private SpriteRenderer _purchasableOverlayRenderer;

    private TextMeshProUGUI _towerCostTMP;
    [SerializeField] private IconFollowMouse _iconFollowMouse;

    private Camera _camera;
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _purchasableOverlayRenderer = transform.Find("Purchasable Overlay").GetComponent<SpriteRenderer>();
        PopulateTextInChildren();
        _camera = Camera.main;
    }

    private void LateUpdate() // Critical that this is late update. Has to resolve after IconFollowMouse update.
    {
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_collider2D.bounds.Contains(mouseWorldPos) && Input.GetMouseButtonDown(0))
        {
            _iconFollowMouse.HandleOnClicked();
        }

        // Check if we can afford the tower. This changes the icon's display.
        if (Economy.Main.CheckCanBuy(CostsData.Main.GetTowerCost(towerPrefab.name)))
        {
            _towerCostTMP.color = Color.yellow;
            _purchasableOverlayRenderer.enabled = false;
        }
        else
        {
            _towerCostTMP.color = Color.red;
            _purchasableOverlayRenderer.enabled = true;
        }

    }

    private void PopulateTextInChildren()
    {
        foreach (var textMeshProUGUI in new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>()))
        {
            switch (textMeshProUGUI.name)
            {
                case "Cost":
                    _towerCostTMP = textMeshProUGUI;
                    textMeshProUGUI.text = CostsData.Main.GetTowerCost(towerPrefab.name).ToString();
                    break;
            }
        }
    }
}
