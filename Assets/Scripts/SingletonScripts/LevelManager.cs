using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SingletonScripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
    
        public static LevelManager Main;
        public Transform startPoint;
        public Transform[] path;
        public Queue<GameObject> Enemies;
        
        public delegate void RoundOverAction();
        public event RoundOverAction OnRoundOver;
        
        public delegate void RoundStartedAction();
        public event RoundStartedAction OnRoundStarted;

        private bool _roundStarted;
        private bool _roundOver;
        private int _currentLevel;

        private int _lives = 2;
        private float _enemySpawnCooldown;
        private int _totalEnemiesToBeSpawned;
        private int[] _totalEnemiesToBeSpawnedByLevel;
        private float[] _enemySpawnTimerByLevel;
        private float _timer; 



        private void Awake()
        {
            // Singleton style
            Main = this;
            
            
            Enemies = new Queue<GameObject>();

            _currentLevel = - 1;
            _totalEnemiesToBeSpawnedByLevel = new[] { 5, 15, 20, 30, 30, 60, 60 };
            _enemySpawnTimerByLevel = new[] { 3f, 1.5f, 1f, 1.5f, .25f, .3f, .15f };
        }

        private void Start()
        {
            _timer = _enemySpawnCooldown;
            Heart.UpdateLives(_lives);
            UpdateLevelInfo();
        }

        private void SpawnEnemy()
        {
            _totalEnemiesToBeSpawned -= 1;
            Instantiate(enemyPrefab, startPoint);
        }

        private void Update()
        {
            if (!_roundStarted || _roundOver) return;
        
            _timer += Time.deltaTime;

            if (_timer > _enemySpawnCooldown && _totalEnemiesToBeSpawned > 0)
            {
                _timer = 0;
                SpawnEnemy();
            }

        }

        public void EnqueueEnemy(GameObject enemy)
        {
            Enemies.Enqueue(enemy);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void DequeueEnemy(bool gotThrough)
        {
            if (gotThrough)
            {
                Heart.UpdateLives(--_lives);
                if (_lives <= 0)
                    SceneManager.LoadScene("GameOver");
            }
            else Economy.Main.IncreaseCoins(1);
        
            Enemies.Dequeue().GetComponent<Enemy>().KillEnemy();
        
            // Check if the round is over
            if (Enemies.Count <= 0 && _totalEnemiesToBeSpawned <= 0 && !_roundOver)
            {
                EndRound();
            }
            
            
        }

        private void EndRound()
        {
            _roundOver = true;
            OnRoundOver?.Invoke();
            GoSign.Main.DisplaySign();
            UpdateLevelInfo();
        }

        public void StartRound()
        {
            OnRoundStarted?.Invoke();
            _roundOver = false;
            _roundStarted = true;
        }

        private void UpdateLevelInfo()
        {
            _currentLevel++;
            _enemySpawnCooldown = _enemySpawnTimerByLevel[_currentLevel];
            _totalEnemiesToBeSpawned = _totalEnemiesToBeSpawnedByLevel[_currentLevel];
            Level.Main.UpdateText(_currentLevel + 1);
        }
        
        public bool RoundOver()
        {
            return _roundOver;
        }
    }
}
