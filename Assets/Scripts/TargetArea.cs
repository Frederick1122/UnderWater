using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{

    [Header("TurrelPreferences")]
    [SerializeField] private Turrel turrelScript;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private float reloadTime;
    [SerializeField] private int maxDamage;
    [SerializeField] private int minDamage;
    [SerializeField] private float lifetimeBullet;

    private bool _reload;

    IEnumerator ReloadCoroutine(float time)
    {
        _reload = true;
        yield return new WaitForSeconds(time);
        _reload = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            if (!turrelScript.target.CompareTag("Enemy"))
            {
                
                other.gameObject.GetComponentInParent<EnemyController>().OnTarget();
                turrelScript.Targetting(other.gameObject);
                if (!_reload)
                {
                    var damage = Random.Range(minDamage, maxDamage);
                    Debug.Log(damage);
                    turrelScript.Attack(other.gameObject, bullet, speedBullet, damage, lifetimeBullet);
                    StartCoroutine(ReloadCoroutine(reloadTime));
                }
            }
            else
            {
                if (other.gameObject == turrelScript.target)
                {
                    if (!_reload)
                    {
                        var damage = Random.Range(minDamage, maxDamage);
                        Debug.Log(damage);
                        turrelScript.Attack(other.gameObject, bullet, speedBullet, damage, lifetimeBullet);
                        StartCoroutine(ReloadCoroutine(reloadTime));
                    }
                }

            }
        }
    }

}
