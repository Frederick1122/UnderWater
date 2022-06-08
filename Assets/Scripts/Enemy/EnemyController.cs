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

    //private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private NavMeshAgent _navMeshAgent;
    private bool _isStay;
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
        //_rigidbody = GetComponentInChildren<Rigidbody2D>();
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
            _isStay = false;
            _navMeshAgent.SetDestination(target.transform.position);
            _navMeshAgent.speed = _speed;
            if (!_animator.GetBool("Walk"))
            {
                _animator.SetBool("Walk", true);
            }
            if (transform.position.x - target.transform.position.x < 0 && !_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = true;
            }
            else if (transform.position.x - target.transform.position.x > 0 && _spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = false;
            }
            return;
        }
        if(!_isStay)
        {
            _isStay = true;
            _navMeshAgent.speed = 0;
            if (_animator.GetBool("Walk"))
            {
                _animator.SetBool("Walk", false);
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
