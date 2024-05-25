using UnityEngine;
using UpdateMenuScripts;

namespace SingletonScripts
{
    public class SelectedTowerMenuDisplay : MonoBehaviour
    {

        private SpriteRenderer _spriteRenderer;
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void OnEnable()
        {
            _spriteRenderer.sprite = UpgradeMenu.Main.UpgradeMenuTower.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
