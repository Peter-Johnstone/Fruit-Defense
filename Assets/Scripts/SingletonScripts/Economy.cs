using TMPro;
using UnityEngine;

namespace SingletonScripts
{
    public class Economy : MonoBehaviour
    {

        public static Economy Main;
    
        [SerializeField] private int coins;
    
        private TextMeshProUGUI _tmp;
    
    
        private void Awake()
        {
            Main = this;

            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            UpdateEconomy();
        }


        private void UpdateEconomy()
        {
            _tmp.text = coins.ToString();
        }


        public void IncreaseCoins(int increase)
        {
            coins += increase;
            UpdateEconomy();
        }

        public bool BuyTower(int cost)
        {
            if (cost > coins) return false;
            coins -= cost;
            UpdateEconomy();
            return true;
        }

        public bool CheckCanBuy(int cost)
        {
            return cost <= coins;
        }
    }
}
