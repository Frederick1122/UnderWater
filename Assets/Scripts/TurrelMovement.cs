using UnityEngine;

public class TurrelMovement : MonoBehaviour
{
    [SerializeField] private GameObject _standartTurrelPosition;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;

    private GameObject _turrelPosition;
    private GameObject _turrelTarget;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _turrelPosition = _standartTurrelPosition
    }

    void FixedUpdate()
    {
        
        if(_turrelTarget != null)
            Rotation(_turrelTarget.transform);
        else
            Rotation(_turrelPosition.transform);

        Movement();
    }

    private void Movement()
    {
        Vector3 target = new Vector3(_turrelPosition.transform.position.x, _turrelPosition.transform.position.y, transform.position.z);

        var a = _player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 ? _player.GetComponent<Rigidbody2D>().velocity.magnitude : _speed;
        transform.position = Vector3.MoveTowards(transform.position, target, a * Time.deltaTime);
    }

    private void Rotation(Transform _targetPosition)
    {
        if (transform.position.x - _targetPosition.position.x < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
        else if (transform.position.x - _targetPosition.position.x > 0 && _spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public GameObject GetTarget()
    {
        return _turrelTarget;
    }
    public void SetTarget(GameObject newTarget)
    {
        _turrelTarget = newTarget;
    }
    public void SetTarget()
    {
        _turrelTarget = null;
    }

}


