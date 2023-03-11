using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class EnemyConfiguration : MonoBehaviour
{
    public Action onEnemyConfigurationEnd;
    public UnityEvent uOnEnemyConfigurationEnd;
    [SerializeField] private EnemyConfigurationData[] enemyConfigurationDatas;
    private List<GameObject> enemies = new List<GameObject>();
    public IEnumerator SpawnConfiguration(float enemySpeedMult)
    {
        enemies.Clear();
        for (int i = 0; i < enemyConfigurationDatas.Length; i++) yield return StartCoroutine(SpawnEnemy(enemyConfigurationDatas[i], enemySpeedMult));
        while (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++) if (enemies[i] == null) enemies.RemoveAt(i);
            yield return null;
        }
        onEnemyConfigurationEnd?.Invoke();
        uOnEnemyConfigurationEnd?.Invoke();
    }
    private IEnumerator SpawnEnemy(EnemyConfigurationData enemyConfigurationData, float enemySpeedMult)
    {
        yield return new WaitForSeconds(enemyConfigurationData.timeOffset);
        GameObject enemy = Instantiate(enemyConfigurationData.enemyPrefab, new Vector3(0, enemyConfigurationData.spawnHeight, 0), Quaternion.identity);
        enemy.GetComponentInChildren<IMovingEnemy>().SpeedMult = enemySpeedMult;
        enemies.Add(enemy);
    }

    [Serializable]
    public struct EnemyConfigurationData
    {
        public GameObject enemyPrefab;
        public float spawnHeight, timeOffset;
    }
}
