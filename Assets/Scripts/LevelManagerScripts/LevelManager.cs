using System.Collections.Generic;
using EnemyScripts;
using LevelManagerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SingletonScripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject gremlin;
        [SerializeField] private GameObject pea;
    
        public static LevelManager Main;
        [HideInInspector] public Transform startPoint;
        [HideInInspector] public Transform[] path;
        [HideInInspector] public List<GameObject> enemies;
        
        public delegate void RoundOverAction();
        public event RoundOverAction OnRoundOver;
        
        public delegate void RoundStartedAction();
        public event RoundStartedAction OnRoundStarted;

        private bool _roundStarted;
        public bool RoundOver { get; private set; }
        private int _currentLevel;

        private int _lives = 2;
        private float _timer;

        private int _currentMiniWaveIndex;
        private MiniWave _currentMiniWave;
        private MiniWave[][] _levelSpawnData;



        private void Awake()
        {
            // Singleton style
            Main = this;
            
            enemies = new List<GameObject>();
            
            _currentLevel = - 1;
            
            // An array of arrays. Each nested array is a miniwave and each outside array is a full wave.
            _levelSpawnData = new[]
            {
                new[] { new MiniWave(gremlin, 5, 3f),  }, // Wave 1 
                
                new[] { new MiniWave(gremlin, 10, 1.5f) }, // Wave 2
                
                new[] { new MiniWave(gremlin, 10, 1f), 
                    new MiniWave(pea, 2, 3), 
                    new MiniWave(gremlin, 10, 1f) }, // Wave 3
                
                new[]
                {
                    new MiniWave(gremlin, 10, 1.1f),
                    new MiniWave(pea, 2, 3), 
                    new MiniWave(gremlin, 15, 1.1f), }, // Wave 4
                
                new[] { new MiniWave(pea, 3, 3), 
                    new MiniWave(gremlin, 15, .25f),
                    new MiniWave(pea, 2, 4),
                    new MiniWave(gremlin, 15, .25f) }, // Wave 5
                
                new[] { new MiniWave(pea, 10, 3), }, // Wave 6
                
                new[] { new MiniWave(gremlin, 80, .15f) } // Wave 7
                
            };
        }

        private void Start()
        {
            _currentMiniWaveIndex = 0;
            _currentMiniWave = _levelSpawnData[0][0];
            
            _timer = _currentMiniWave.TimingBetweenSpawns;
            
            Heart.UpdateLives(_lives);
            UpdateLevelInfo();
        }

        private void Update()
        {
            if (!_roundStarted || RoundOver) return;
            
            _timer += Time.deltaTime;

            if (_timer > _currentMiniWave.TimingBetweenSpawns && _currentMiniWave.NumberToSpawn > 0)
            {
                _timer = 0;
                SpawnEnemy();
            }
        }

        private void CheckNewMiniWave()
        {
            if (_currentMiniWave.NumberToSpawn != 0) return;

            if (++_currentMiniWaveIndex >= _levelSpawnData[_currentLevel].Length)
            {
                if (!RoundOver && enemies.Count <= 0)
                    EndRound();
            }
            else
                _currentMiniWave = _levelSpawnData[_currentLevel][_currentMiniWaveIndex];
        }
        
        private void SpawnEnemy()
        {
            Instantiate(_currentMiniWave.GetEnemyPrefab(), startPoint);
        }

        public void AddEnemyToShootableEnemies(GameObject enemy)
        {
            // Puts the enemy inside the list that the towers select their targets from.
            enemies.Add(enemy);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void RemoveEnemy(bool gotThrough, GameObject enemy)
        {
            if (gotThrough)
            {
                // If enemy got through, decrease lives and check game over.
                Heart.UpdateLives(--_lives);
                if (_lives <= 0)
                    SceneManager.LoadScene("GameOver");
            }
            else Economy.Main.IncreaseCoins(1); // enemy didn't get through --> increase gold.

            enemies.Remove(enemy);
            enemy.GetComponent<BaseEnemy>().TriggerDeath();
        
            // Check if the round is over or new miniwaves
            CheckNewMiniWave();
            
            
        }

        public void StartRound()
        {
            OnRoundStarted?.Invoke();
            RoundOver = false;
            _roundStarted = true;
        }
        
        private void EndRound()
        {
            RoundOver = true;
            OnRoundOver?.Invoke();
            GoSign.Main.DisplaySign();
            UpdateLevelInfo();
        }

        private void UpdateLevelInfo()
        {
            _currentLevel++;
            _currentMiniWaveIndex = 0;
            _currentMiniWave = _levelSpawnData[_currentLevel][0];
            
            Level.Main.UpdateText(_currentLevel + 1);
        }
    }
}
