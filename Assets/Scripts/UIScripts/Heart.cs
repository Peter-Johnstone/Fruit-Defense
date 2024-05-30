using TMPro;
using UnityEngine;

namespace SingletonScripts
{
    public class Heart : MonoBehaviour
    {

        private static TextMeshProUGUI _tmp; 
    
        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }

    
        public static void UpdateLives(int lives)
        {
            _tmp.text = lives.ToString();
        }
    }
}
