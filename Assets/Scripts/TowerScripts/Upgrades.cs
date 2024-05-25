using UnityEngine;
using UpdateMenuScripts;

namespace TowerScripts
{
    public class Upgrades : MonoBehaviour
    {
        [SerializeField] private UpgradeTree upgradeTree;

        public delegate void UpgradeAction();
        public event UpgradeAction OnUpgrade;
        public int LeftUpgradePathLevel { get; private set; }
        public int RightUpgradePathLevel { get; private set; }
    
        public string LeftUpgrade { get; private set; }
        public string RightUpgrade { get; private set; }
    
        public void Awake()
        {
            LeftUpgrade = upgradeTree.leftUpgrades[0];
            RightUpgrade = upgradeTree.rightUpgrades[0];
        }
        public void UpgradeLeft()
        {
            LeftUpgradePathLevel++;
            LeftUpgrade = upgradeTree.leftUpgrades[LeftUpgradePathLevel];
            LeftUpgradeText.SetUpgradedText();
            OnUpgrade?.Invoke();

        }

        public void UpgradeRight()
        {
            RightUpgradePathLevel++;
            RightUpgrade = upgradeTree.rightUpgrades[RightUpgradePathLevel];
            RightUpgradeText.SetUpgradedText();
            OnUpgrade?.Invoke();
        }
    }
}
