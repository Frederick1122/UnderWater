using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject enemy;
    private Transform enemyParent;
    private void Start()
    {
        enemyParent = transform.GetChild(0).transform;
    }
    public void Spawner()
    {
        GameObject enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
        enemyInstance.transform.parent = enemyParent;
    }
}
