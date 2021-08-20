using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetArea : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            enemyController.target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyController.target = null;
        }
    }

}
