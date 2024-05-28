using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UpdateMenuScripts;

public class DamageDisplay : MonoBehaviour
{

    private static TextMeshProUGUI _tmp;

    private static Stats _stats;

    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _stats = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        _tmp.text = _stats.TotalDamage.ToString();
    }

    public static void UpdateRelevantTowerStats()
    {
        _stats = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Stats>();
    }
}
