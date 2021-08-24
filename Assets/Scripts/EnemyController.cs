using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    [SerializeField] private Animator animator;
    public SpriteRenderer spriteRenderer;
    [NonSerialized] public NavMeshAgent navMeshAgent;
    private bool _animFlag;
    private bool _rotateFlag;
    IEnumerator DamageCoroutine(float t)
    {
        while (t < 1.0f)
        {
            spriteRenderer.color = gradient.Evaluate(t);
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        _rb = GetComponent<Rigidbody2D>();
        
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
            navMeshAgent.SetDestination(target.transform.position);
            
            if (!_animFlag)
            {
                animator.SetBool("Walk", true);
                _animFlag = true;
            }
            if (transform.position.x - target.transform.position.x < 0 && !_rotateFlag)
            {
                _rotateFlag = true;
                spriteRenderer.flipX = _rotateFlag;
            }
            else if (transform.position.x - target.transform.position.x > 0 && _rotateFlag)
            {
                _rotateFlag = false;
                spriteRenderer.flipX = _rotateFlag;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
            if (_animFlag)
            {
                animator.SetBool("Walk", false);
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
