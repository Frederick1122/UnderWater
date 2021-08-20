using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]private GameObject enemy;

    public void Spawner()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
