using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField][Range(.5f, 4f)] private float enemyMultipliedSpeedPerMinute;
    [SerializeField] private EnemyConfiguration[] enemyConfigurations;
    private float enemySpeedMult = 1f;
    private void Start()
    {
        Instantiate(enemyConfigurations[0], new Vector3(0, 0, 0), Quaternion.identity);
    }
    private void Update()
    {
        enemySpeedMult += enemyMultipliedSpeedPerMinute * Time.deltaTime / 60f;
    }
}
