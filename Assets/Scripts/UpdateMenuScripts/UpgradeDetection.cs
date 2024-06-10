using System.Collections;
using System.Collections.Generic;
using TowerScripts;
using UnityEngine;
using UpdateMenuScripts;

public class UpgradeDetection : MonoBehaviour
{

    [SerializeField] private Sprite notSelectedImage;
    [SerializeField] private Sprite selectedImage;
    
    public SpriteRenderer upgradeLevelBoxUpper;
    public SpriteRenderer upgradeLevelBoxLower;
    public Sprite filledUpgradeBox;
    public  Sprite unfilledUpgradeBox;
    
    public SpriteRenderer upgradeArrowRenderer;
    public Sprite arrowSprite;
    public Sprite checkmarkSprite;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>(); 
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_collider2D.bounds.Contains(mouseWorldPos))
        {
            _spriteRenderer.sprite = selectedImage;
            if (Input.GetMouseButtonDown(0))
            {
                if (gameObject.name == "Left Upgrade")
                    UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Upgrades>().UpgradeLeft();
                else if (gameObject.name == "Right Upgrade")
                    UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Upgrades>().UpgradeRight();
                    
            }
        }
        else 
            _spriteRenderer.sprite = notSelectedImage;
    }
}
