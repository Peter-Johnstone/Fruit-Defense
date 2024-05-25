using TMPro;
using TowerScripts;
using UnityEngine;
using UpdateMenuScripts;

namespace UpdateMenuScripts
{
    public class RightUpgradeText : MonoBehaviour
    {
        private static TextMeshProUGUI _tmp;
        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }
        
        public static void SetUpgradedText()
        { 
            _tmp.text = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<Upgrades>().RightUpgrade;
        }
    }
}