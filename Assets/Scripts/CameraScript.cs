using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;
    [SerializeField] private bool itsTurrel;


    void FixedUpdate()
    {
       
        var _target = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
      
        var heading = transform.position - _target;
        
        if (heading.sqrMagnitude < maxDistance * maxDistance)

        {
            
                transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
            
        }
        else
        {


            var a = player.GetComponent<Rigidbody2D>().velocity.magnitude > 0 ? player.GetComponent<Rigidbody2D>().velocity.magnitude : speed;


            transform.position = Vector3.MoveTowards(transform.position, _target, a * Time.deltaTime);
        }
        
    }
}
