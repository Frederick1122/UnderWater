using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Joystick _moveJoystick;

    private Rigidbody2D _rigidBody;
    private bool _flip;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveJoystick = UIManager.Instance.GetJoystick();
        _flip = true;
    }
    void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        var xMovement = _moveJoystick.Horizontal;
        var yMovement = _moveJoystick.Vertical;
        Vector3 movement = new Vector3(xMovement, yMovement, transform.position.z);
        if (movement != new Vector3(0, 0, transform.position.z) && !_animator.GetBool("isWalk"))
        {
            _animator.SetBool("isWalk", true);
        }
        else if (movement == new Vector3(0, 0, transform.position.z) && _animator.GetBool("isWalk"))
        {
            _animator.SetBool("isWalk", false);
        }
        _rigidBody.velocity = movement * speed * Time.fixedDeltaTime;
        if (xMovement < 0 && !_flip)
        {
            _spriteRenderer.flipX = _flip;
            _flip = true;
        }
        else if (xMovement > 0 && _flip)
        {
            _spriteRenderer.flipX = _flip;
            _flip = false;
        }
    }
}
