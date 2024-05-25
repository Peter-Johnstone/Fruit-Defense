using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerScripts;
using UnityEngine;
using UpdateMenuScripts;

public class LeftUpgradeText : MonoBehaviour
{

    private static TextMeshProUGUI _tmp;
    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    public static void SetUpgradedText()
    {
        _tmp.text = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Upgrades>().LeftUpgrade;
    }
}
