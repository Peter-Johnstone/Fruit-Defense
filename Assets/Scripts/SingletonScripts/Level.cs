using TMPro;
using UnityEngine;

namespace SingletonScripts
{
    public class Level : MonoBehaviour
    {

        public static Level Main;

        private TextMeshProUGUI _tmp;
    
        // Start is called before the first frame update
        private void Awake()
        {
            Main = this;
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateText(int level)
        {
            _tmp.text = level.ToString();
        }
    }
}
