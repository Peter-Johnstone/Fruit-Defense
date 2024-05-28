using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeTree : ScriptableObject
{
    public string[] leftUpgrades;

    public string[] rightUpgrades;

    public int[] leftUpgradeCosts;

    public int[] rightUpgradeCosts;
}
