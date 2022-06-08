﻿using UnityEngine;

public class TurrelController : MonoBehaviour
{
    //TODO: разнести этот скрипт по нескольким
    [SerializeField] private GameObject _standartTarget;
    [SerializeField] private GameObject _player;
    
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;

    private GameObject _target;
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
            }
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public GameObject GetTarget()
    {
        return _target;
    }

    void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (this._target == null)
        {
            this._target = _standartTarget;
            _turrelInEnemyFlag = false;
        }
        if (transform.position.x - this._target.transform.position.x < 0 && !_turrelRotateFlag)
        {
            _turrelRotateFlag = true;
            _spriteRenderer.flipX = _turrelRotateFlag;
            
        }
        else if (transform.position.x - this._target.transform.position.x > 0 && _turrelRotateFlag)
        {
            _turrelRotateFlag = false;
            _spriteRenderer.flipX = _turrelRotateFlag;

           
        }
        var _target = new Vector3(this._target.transform.position.x, this._target.transform.position.y, transform.position.z);
        var heading = transform.position - _target;
        var enemyControl = this._target.GetComponent<EnemyController>() != null ? this._target.GetComponent<EnemyController>() : this._target.GetComponentInParent<EnemyController>() != null ? this._target.GetComponentInParent<EnemyController>() : null;

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
            var a = _player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 ? _player.GetComponent<Rigidbody2D>().velocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, _target, a * Time.deltaTime);
        }
    }

    public void Targetting(GameObject _target)
    {
        this._target = _target;
    }
    public void Attack(GameObject _target, GameObject bullet, float speedBullet, int damage, float lifetimeBullet)
    {
        var go = Instantiate(bullet, transform.position, Quaternion.identity);
        var bulletScript = go.GetComponent<Bullet>();
        bulletScript.target = _target.transform.position;
        bulletScript.speed = speedBullet;
        bulletScript.damage = damage;
        bulletScript.lifetime = lifetimeBullet;
    }
}

