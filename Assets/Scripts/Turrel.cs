using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{


    private GameObject _standartTarget;
    public GameObject player;
    public GameObject target;
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;

    private bool _turrelRotateFlag;
    private bool _turrelInEnemyFlag;
    private SpriteRenderer _spriteRenderer;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyTurrelArea"))
        {
            if (other.GetComponentInParent<EnemyController>().triggerForTurrel == other.gameObject.GetComponent<CircleCollider2D>())
            {
                _turrelInEnemyFlag = true;
                Debug.Log("Yes");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTurrelArea"))
        {
            if (collision.GetComponentInParent<EnemyController>().triggerForTurrel == collision.gameObject.GetComponent<CircleCollider2D>())
            {
                _turrelInEnemyFlag = false;
                Debug.Log("No");

            }

        }
    }

    private void Start()
    {
        _standartTarget = target;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        MovementLogic();

    }

    private void MovementLogic()
    {
        if (target == null)
        {
            target = _standartTarget;
            _turrelInEnemyFlag = false;
        }
        if (transform.position.x - target.transform.position.x < 0 && !_turrelRotateFlag)
        {
            _turrelRotateFlag = true;
            _spriteRenderer.flipX = _turrelRotateFlag;
            
        }
        else if (transform.position.x - target.transform.position.x > 0 && _turrelRotateFlag)
        {
            _turrelRotateFlag = false;
            _spriteRenderer.flipX = _turrelRotateFlag;

           
        }
        var _target = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        var heading = transform.position - _target;
        var enemyControl = target.GetComponent<EnemyController>() != null ? target.GetComponent<EnemyController>() : target.GetComponentInParent<EnemyController>() != null ? target.GetComponentInParent<EnemyController>() : null;

        if (enemyControl != null && !_turrelInEnemyFlag)
        {
            if (heading.sqrMagnitude < maxDistance * maxDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, speed * 2 * Time.deltaTime);
            }
        }
        else if(enemyControl == null)
        {
            var a = player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 ? player.GetComponent<Rigidbody2D>().velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, _target, a * Time.deltaTime);
        }
    }

    public void Targetting(GameObject _target)
    {
        target = _target;
    }
    public void Attack(GameObject _target, GameObject bullet, float speedBullet, int damage, float lifetimeBullet)
    {
        var go = Instantiate(bullet, transform.position, Quaternion.identity);
        var bulletScript = go.GetComponent<Bullet>();
        bulletScript.target = _target.transform.position;
        bulletScript.speed = speedBullet;
        bulletScript.damage = damage;
        bulletScript.lifetime = lifetimeBullet;
        

        //target.GetComponent<EnemyController>().OnTarget();
    }
}


