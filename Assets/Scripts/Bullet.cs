using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [NonSerialized] public int damage;
    [NonSerialized] public Vector3 target;
    [NonSerialized] public float speed;
    [NonSerialized] public float lifetime;
    [SerializeField] GameObject DeathBullet;
    private Rigidbody2D _rb;
    

    IEnumerator CoroutineOfDeath()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyController>().TakeDamage(damage);
            Instantiate(DeathBullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        _rb.velocity = (target - transform.position).normalized * speed;
        StartCoroutine(CoroutineOfDeath());
    }
    private void FixedUpdate()
    {
        //transform.Translate((target - transform.position) * speed * Time.deltaTime);
        
    }
   
}
