using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine;

public class EnemyConfiguration : MonoBehaviour
{
    public Action onEnemyConfigurationEnd;
    public UnityEvent uOnEnemyConfigurationEnd;
    [SerializeField] private EnemyConfigurationData[] enemyConfigurationDatas;
    public int difficulty; // 0 = easy, 1 = medium, 2 = hard, 3 = boss

    private void Start()
    {
        StartCoroutine(SpawnConfiguration());
    }
    private IEnumerator SpawnConfiguration()
    {
        for (int i = 0; i < enemyConfigurationDatas.Length; i++) yield return StartCoroutine(SpawnEnemy(enemyConfigurationDatas[i]));
        onEnemyConfigurationEnd?.Invoke();
        uOnEnemyConfigurationEnd?.Invoke();
    }
    private IEnumerator SpawnEnemy(EnemyConfigurationData enemyConfigurationData)
    {
        yield return new WaitForSeconds(enemyConfigurationData.timeOffset);
        Instantiate(enemyConfigurationData.enemyPrefab, new Vector3(0, enemyConfigurationData.spawnHeight, 0), Quaternion.identity);
    }

    [Serializable]
    public struct EnemyConfigurationData
    {
        public GameObject enemyPrefab;
        public float spawnHeight, timeOffset;
    }
}
