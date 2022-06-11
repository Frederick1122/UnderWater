using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //Kostyl
    [SerializeField] string _UIScene;
    //Kostyl
    [SerializeField] private float _speed;

    private Joystick _moveJoystick;

    private Rigidbody2D _rigidBody;
    private bool _flip;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private IEnumerator Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveJoystick = UIManager.Instance?.GetJoystick();
        //Kostyl
        if(_moveJoystick == null)
        {
            SceneManager.LoadSceneAsync(_UIScene, LoadSceneMode.Additive);
            yield return new WaitForSeconds(0.5f);
            _moveJoystick = UIManager.Instance?.GetJoystick();
        }
        Debug.Log(_moveJoystick);
        //Kostyl
        _flip = true;
    }

    void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (_moveJoystick == null) return;
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
        _rigidBody.velocity = movement * _speed * Time.fixedDeltaTime;
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
