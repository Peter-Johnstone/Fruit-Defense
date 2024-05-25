using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SingletonScripts
{
    public class CostsData : MonoBehaviour
    {
        [SerializeField] private int archerCost;

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
            }

            return -1;
        }
    }
}
