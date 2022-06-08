using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private Vector3 _target;
    private float _speed;
    private float _lifetime;
    [SerializeField] GameObject DeathBullet;
    private Rigidbody2D _rb;
    
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyController>().TakeDamage(_damage);
            Instantiate(DeathBullet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        _rb.velocity = (_target - transform.position).normalized * _speed;
        StartCoroutine(DeathRoutine());
    }

    public void InstatiateBullet(int damage, Vector3 target, float speed, float lifetime)
    {
        _damage = damage;
        _target = target;
        _speed = speed;
        _lifetime = lifetime;
    }
}
