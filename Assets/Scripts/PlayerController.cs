using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Joystick moveJoystick;
    public Joystick fireJoystick;
    public GameObject weapon;
    public float speed;




    private Rigidbody2D _rb;
    //private float _angle;
    private bool _flip;
    private Animator _animator;
  


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //_angle = -1000;
        _flip = true;

    }

    private void Update()
    {
        // WeaponLogic();
    }
    void FixedUpdate()
    {
        MovementLogic();

    }

    private void MovementLogic()
    {
        var x = moveJoystick.Horizontal;
        var y = moveJoystick.Vertical;
        Vector3 movement = new Vector3(x, y, transform.position.z);
        if (movement != new Vector3(0, 0, transform.position.z) && !_animator.GetBool("isWalk"))
        {
            _animator.SetBool("isWalk", true);
        }
        else if (movement == new Vector3(0, 0, transform.position.z) && _animator.GetBool("isWalk"))
        {
            _animator.SetBool("isWalk", false);
        }
        _rb.velocity = movement * speed * Time.fixedDeltaTime;
        if (x < 0 && !_flip)
        {
            transform.Rotate(Vector3.up, 180);
            _flip = true;
        }
        else if (x > 0 && _flip)
        {
            transform.Rotate(Vector3.up, 180);
            _flip = false;
        }
    }
    /*private void WeaponLogic()
    {
        var a = (fireJoystick.Vertical > 0 ? (int)Vector3.Angle(transform.right, fireJoystick.Direction) : -(int)Vector3.Angle(transform.right, fireJoystick.Direction));
        Debug.Log(a);
       
        if (_angle == -1000)
        {
            weapon.transform.Rotate(Vector3.up, a);
            _angle = a;
        }
        else if (a != _angle && fireJoystick.Vertical + fireJoystick.Horizontal != 0)
        {
            weapon.transform.Rotate(Vector3.forward, a - _angle);
            _angle = a;
           
        }
        if (fireJoystick.Horizontal < 0 && !_flip)
        {
            gameObject.transform.Rotate(Vector3.up, 180);
           
            _flip = true;
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -2);

        }
        else if (fireJoystick.Horizontal > 0 && _flip)
        {
            gameObject.transform.Rotate(Vector3.up, 180);
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -1);

            _flip = false;
        }
    }*/
}
