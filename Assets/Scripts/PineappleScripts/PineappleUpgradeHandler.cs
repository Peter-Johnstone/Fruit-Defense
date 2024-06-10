using System.Collections;
using System.Collections.Generic;
using TowerScripts;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PineappleUpgradeHandler : MonoBehaviour
{

    private Range _range;
    private Hair _hair;
    private Upgrades _upgrades;

    [SerializeField] private SpriteLibrary pineappleSpriteLibrary;
    [SerializeField] private SpriteLibraryAsset pineappleUpgradedRange;
    [SerializeField] private SpriteRenderer spriteRendererOfPineappleUpgradeLevel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _range = GetComponentInChildren<Range>();
        _hair = GetComponentInChildren<Hair>();
        _upgrades = GetComponent<Upgrades>();
        _upgrades.OnUpgrade += HandleUpgrade;
    }

    // Update is called once per frame
    void HandleUpgrade()
    {
        switch (_upgrades.LeftUpgradePathLevel)
        {
            case 1: // Range Upgrade
                _range.UpgradeRange();
                pineappleSpriteLibrary.spriteLibraryAsset = pineappleUpgradedRange;
                break;
            case 2:
                // Do something 
                break;
        }

        switch (_upgrades.RightUpgradePathLevel)
        {
            case 1: // Hot Hair
                _hair.UpgradeHotHair();
                break;
        }
        spriteRendererOfPineappleUpgradeLevel.sprite = pineappleSpriteLibrary.spriteLibraryAsset.GetSprite("UpgradeIcon", "UpgradeIcon");
    }
}
