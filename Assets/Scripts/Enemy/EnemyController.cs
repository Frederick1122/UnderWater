using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public CircleCollider2D triggerForTurrel;
    [Space]

    [SerializeField] private GameObject _targetIcon;
    [SerializeField] private int _hp;
    [SerializeField] private float _speed;
    [SerializeField] private Gradient _gradient;

    [NonSerialized] public GameObject target;

    private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private NavMeshAgent _navMeshAgent;
    private bool _animFlag;
    private bool _rotateFlag;
    IEnumerator DamageCoroutine(float t)
    {
        while (t < 1.0f)
        {
            _spriteRenderer.color = _gradient.Evaluate(t);
            t += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _animFlag = false;
    }
    void Update()
    {
        if (_hp <= 0)
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
            _navMeshAgent.SetDestination(target.transform.position);
            
            if (!_animFlag)
            {
                _animator.SetBool("Walk", true);
                _animFlag = true;
            }
            if (transform.position.x - target.transform.position.x < 0 && !_rotateFlag)
            {
                _rotateFlag = true;
                _spriteRenderer.flipX = _rotateFlag;
            }
            else if (transform.position.x - target.transform.position.x > 0 && _rotateFlag)
            {
                _rotateFlag = false;
                _spriteRenderer.flipX = _rotateFlag;
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            if (_animFlag)
            {
                _animator.SetBool("Walk", false);
                _animFlag = false;
            }

        }

    }
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        StartCoroutine(DamageCoroutine(0f));
    }
    public void DeathEnemy()
    {
        Destroy(gameObject);
    }
    public void CaughtInCrosshair()
    {
        _targetIcon.SetActive(true);
    }
}
