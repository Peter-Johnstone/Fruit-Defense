using SingletonScripts;
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


        
        
        public bool LeftUpgradePathFinished { get; private set; }
        
        public bool RightUpgradePathFinished { get; private set; }
        
        public void Awake()
        {
            LeftUpgrade = upgradeTree.leftUpgrades[0];
            RightUpgrade = upgradeTree.rightUpgrades[0];
            
        }
        public void UpgradeLeft()
        {
            // Check if we can buy and that we haven't already bought the last upgrade.
            if (LeftUpgradePathFinished || !Economy.Main.CheckCanBuy(upgradeTree.leftUpgradeCosts[LeftUpgradePathLevel])) return;
            
            Economy.Main.Buy(upgradeTree.leftUpgradeCosts[LeftUpgradePathLevel]);
            LeftUpgradePathLevel++;
            if (LeftUpgradePathLevel == upgradeTree.leftUpgrades.Length)
            {
                LeftUpgradePathFinished = true;
            } else 
                LeftUpgrade = upgradeTree.leftUpgrades[LeftUpgradePathLevel];
            
            LeftUpgradeIcon.SetUpgradeLeftIcon();
            OnUpgrade?.Invoke();

        }

        public void UpgradeRight()
        {
            // Check if we can buy and that we haven't already bought the last upgrade.
            if (RightUpgradePathFinished || !Economy.Main.CheckCanBuy(upgradeTree.rightUpgradeCosts[RightUpgradePathLevel])) return;
            
            Economy.Main.Buy(upgradeTree.rightUpgradeCosts[RightUpgradePathLevel]);
            RightUpgradePathLevel++;
            if (RightUpgradePathLevel == upgradeTree.rightUpgrades.Length)
            {
                RightUpgradePathFinished = true;
            } else 
                RightUpgrade = upgradeTree.rightUpgrades[RightUpgradePathLevel];
            
            RightUpgradeIcon.SetUpgradeRightIcon();
            OnUpgrade?.Invoke();

        }
    }
}
