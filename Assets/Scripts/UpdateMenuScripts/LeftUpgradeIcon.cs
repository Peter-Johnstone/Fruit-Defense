using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerScripts;
using UnityEngine;
using UpdateMenuScripts;

public class LeftUpgradeIcon : MonoBehaviour
{
    private static SpriteRenderer _upgradeArrowRenderer;
    private static Sprite _arrowSprite;
    private static Sprite _checkmarkSprite;
    
    private static TextMeshProUGUI _tmp;
    
    private static SpriteRenderer _upgradeLevelBoxUpper;
    private static SpriteRenderer _upgradeLevelBoxLower;
    private static Sprite _filledUpgradeBox;
    private static Sprite _unfilledUpgradeBox;
    
    private void Awake()
    {
        _upgradeLevelBoxUpper = GetComponentInParent<UpgradeDetection>().upgradeLevelBoxUpper;
        _upgradeLevelBoxLower = GetComponentInParent<UpgradeDetection>().upgradeLevelBoxLower;
        _unfilledUpgradeBox = GetComponentInParent<UpgradeDetection>().unfilledUpgradeBox;
        _filledUpgradeBox = GetComponentInParent<UpgradeDetection>().filledUpgradeBox;
        
        _upgradeArrowRenderer = GetComponentInParent<UpgradeDetection>().upgradeArrowRenderer;
        _arrowSprite = GetComponentInParent<UpgradeDetection>().arrowSprite;
        _checkmarkSprite = GetComponentInParent<UpgradeDetection>().checkmarkSprite;
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    public static void SetUpgradeLeftIcon()
    {
        Upgrades currentTowerUpgrades = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Upgrades>();
        if (currentTowerUpgrades.LeftUpgradePathFinished)
        {
            _upgradeArrowRenderer.sprite = _checkmarkSprite;
            _tmp.text = "Max";
        }
        else
        {
            _upgradeArrowRenderer.sprite = _arrowSprite; 
            _tmp.text = currentTowerUpgrades.LeftUpgrade;
        }

        switch (currentTowerUpgrades.LeftUpgradePathLevel)
        {
            case 2:
                _upgradeLevelBoxUpper.sprite = _filledUpgradeBox;
                _upgradeLevelBoxLower.sprite = _filledUpgradeBox;
                break;
            case 1:
                _upgradeLevelBoxUpper.sprite = _unfilledUpgradeBox;
                _upgradeLevelBoxLower.sprite = _filledUpgradeBox;
                break;
            case 0:
                _upgradeLevelBoxUpper.sprite = _unfilledUpgradeBox;
                _upgradeLevelBoxLower.sprite = _unfilledUpgradeBox;
                break;
        }
            
    }


}
