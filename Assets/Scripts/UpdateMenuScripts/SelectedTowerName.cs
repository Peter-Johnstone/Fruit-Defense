using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UpdateMenuScripts;

public class SelectedTowerName : MonoBehaviour
{
    private TextMeshProUGUI _tmp;
    private Dictionary<string, Color> _nameColorDictionary;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    private void OnEnable()
    {
        // remove clone from name.
        String s = UpgradeMenu.Main.UpgradeMenuTower.name;
        string text = s.Substring(0, s.Length - 7);

        // Set the text to the name
        _tmp.text = text;
    }
}
