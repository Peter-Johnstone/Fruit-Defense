using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SingletonScripts
{
    public class CostsData : MonoBehaviour
    {
        [SerializeField] private int archerCost;
        [SerializeField] private int pineappleCost;

        public static CostsData Main { get; private set; }

        private void Awake()
        {
            Main = this;
        }



        public int GetTowerCost(String towerName)
        {
            switch (towerName)
            {
                case "Archer":
                    return archerCost;
                case "Pineapple":
                    return pineappleCost;
            }

            return -1;
        }
    }
}
