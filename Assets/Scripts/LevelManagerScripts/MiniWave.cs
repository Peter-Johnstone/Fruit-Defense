using UnityEngine;

namespace LevelManagerScripts
{
    public class MiniWave
    {
        private GameObject _enemyPrefab;
        public int NumberToSpawn { get; private set; }
        public float TimingBetweenSpawns { get; private set; }
        
        public MiniWave(GameObject enemyPrefab, int numberToSpawn, float timingBetweenSpawns)
        {
            _enemyPrefab = enemyPrefab;
            NumberToSpawn = numberToSpawn;
            TimingBetweenSpawns = timingBetweenSpawns;
        }

        public GameObject GetEnemyPrefab()
        {
            NumberToSpawn -= 1;
            return _enemyPrefab;
        }
    }
}