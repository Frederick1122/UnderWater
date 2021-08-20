using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CircleCollider2D triggerForTurrel;
    [Space]

    public GameObject targetIcon;
    public int hp;
    [SerializeField] private float speed;
    [SerializeField] private Gradient gradient;


    [NonSerialized] public GameObject target;
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private bool _animFlag;
    private bool _rotateFlag;
    IEnumerator DamageCoroutine(float t)
    {
        while (t < 1f)
        {
            _spriteRenderer.color = gradient.Evaluate(t);
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _animFlag = false;
    }
    void Update()
    {
        if (hp <= 0)
        {
            DeathEnemy();
        }
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }
    private void MovementLogic()
    {

        if (target != null)
        {
            _rb.velocity = (target.transform.position - transform.position) * speed * Time.fixedDeltaTime;
            if (!_animFlag)
            {
                _anim.SetBool("Walk", true);
                _animFlag = true;
            }
            if (transform.position.x - target.transform.position.x < 0 && !_rotateFlag)
            {
                transform.Rotate(Vector3.up, 180);
                _rotateFlag = true;
            }
            else if (transform.position.x - target.transform.position.x > 0 && _rotateFlag)
            {
                transform.Rotate(Vector3.up, 180);
                _rotateFlag = false;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
            if (_animFlag)
            {
                _anim.SetBool("Walk", false);
                _animFlag = false;
            }

        }

    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        StartCoroutine(DamageCoroutine(0f));
    }
    public void DeathEnemy()
    {
        Destroy(gameObject);
    }
    public void OnTarget()
    {
        targetIcon.SetActive(true);
    }
}
