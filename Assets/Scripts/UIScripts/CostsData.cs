using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SingletonScripts
{
    public class CostsData : MonoBehaviour
    {
        [SerializeField] private int archerCost;
        [SerializeField] private int pineappleCost;
        [SerializeField] private int avocadoCost;

        public static CostsData Main { get; private set; }

        private void Awake()
        {
            Main = this;
        }



        public int GetTowerCost(String towerName)
        {
            switch (towerName)
            {
                case "Orange":
                    return archerCost;
                case "Pineapple":
                    return pineappleCost;
                case "Avocado":
                    return avocadoCost;
            }

            return -1;
        }
    }
}
