using System.Collections;
using System.Collections.Generic;
using TowerScripts;
using UnityEngine;

public class PineappleUpgradeHandler : MonoBehaviour
{

    private Range _range;
    private Upgrades _upgrades;
    
    // Start is called before the first frame update
    void Start()
    {
        _range = GetComponentInChildren<Range>();
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
                break;
            case 2:
                // Do something 
                break;
        }
    }
}
