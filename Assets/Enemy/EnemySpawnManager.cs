using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float startWaitBtwConfiguations, reducedWaitPerConfiguration, addedSpeedPerConfiguration, maxEnemySpeedMult;
    [Space]
    [SerializeField][Range(0, 1)] private float easyConfigChance = 0.4f, mediumConfigChance = 0.3f, hardConfigChance = 0.2f, bossConfigChance = 0.1f;
    [SerializeField] private bool easyConfigChanceLocked, mediumConfigChanceLocked, hardConfigChanceLocked, bossConfigChanceLocked;
    [SerializeField] private EnemyConfiguration[] easyEnemyConfigurations, mediumEnemyConfigurations, hardEnemyConfigurations, bossEnemyConfigurations;
    [SerializeField] private EnemyConfiguration testEnemyConfiguration;

    private Coroutine spawnLoopCoroutine;
    private void Start() => spawnLoopCoroutine = StartCoroutine(SpawnLoop(1, startWaitBtwConfiguations));
    private IEnumerator SpawnLoop(float currentEnemySpeedMult, float currentWaitBtwConfigurations)
    {
        while (true)
        {
            yield return new WaitForSeconds(currentWaitBtwConfigurations);

            EnemyConfiguration[] enemyConfigurations = GetEnemyConfigurations();
            EnemyConfiguration enemyConfiguration = Instantiate(enemyConfigurations[Random.Range(0, enemyConfigurations.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            print(enemyConfiguration.name);
            yield return StartCoroutine(enemyConfiguration.SpawnConfiguration(currentEnemySpeedMult));

            currentEnemySpeedMult = Mathf.Min(currentEnemySpeedMult + addedSpeedPerConfiguration, maxEnemySpeedMult);
            currentWaitBtwConfigurations = Mathf.Max(currentWaitBtwConfigurations - reducedWaitPerConfiguration, 0);
        }
    }
    private EnemyConfiguration[] GetEnemyConfigurations()
    {
        if (testEnemyConfiguration != null) return new EnemyConfiguration[] { testEnemyConfiguration };
        float value = Random.value;
        if (value < easyConfigChance) return easyEnemyConfigurations;
        else if (value < easyConfigChance + mediumConfigChance) return mediumEnemyConfigurations;
        else if (value < easyConfigChance + mediumConfigChance + hardConfigChance) return hardEnemyConfigurations;
        else return bossEnemyConfigurations;
    }
    private void OnValidate()
    {
        float sum = easyConfigChance + mediumConfigChance + hardConfigChance + bossConfigChance;
        if (sum > 1)
        {
            float diff = sum - 1;
            if (!easyConfigChanceLocked) easyConfigChance -= diff / 4;
            if (!mediumConfigChanceLocked) mediumConfigChance -= diff / 4;
            if (!hardConfigChanceLocked) hardConfigChance -= diff / 4;
            if (!bossConfigChanceLocked) bossConfigChance -= diff / 4;
        }
        else if (sum < 1)
        {
            float diff = 1 - sum;
            if (!easyConfigChanceLocked) easyConfigChance += diff / 4;
            if (!mediumConfigChanceLocked) mediumConfigChance += diff / 4;
            if (!hardConfigChanceLocked) hardConfigChance += diff / 4;
            if (!bossConfigChanceLocked) bossConfigChance += diff / 4;
        }
    }
}
