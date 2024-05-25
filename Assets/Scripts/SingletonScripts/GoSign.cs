using UnityEngine;

namespace SingletonScripts
{
    public class GoSign : MonoBehaviour
    {
    
        private Collider2D _collider2D;
        private SpriteRenderer _spriteRenderer;

        public static GoSign Main;
        private void Start()
        {
            Main = this;
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
        private void Update()
        {
            if (!_spriteRenderer.enabled) return;
        
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            if (_collider2D.bounds.Contains(mouseWorldPos) && Input.GetMouseButtonDown(0))
            {
                LevelManager.Main.StartRound();
                _spriteRenderer.enabled = false;
            } 
        
        }

        public void DisplaySign()
        {
            _spriteRenderer.enabled = true;
        }
    }
}
