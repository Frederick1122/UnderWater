using System.Collections;
using UnityEngine;

public class TurrelAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject _bullet;

    [Header("BulletPreferences")]
    [Space(5)]
    [SerializeField] private float _speedBullet;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _lifetimeBullet;
    [Space(5)]
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _minDamage;

    private TurrelMovement _turrelMovement;
    private bool _reload;

    IEnumerator ReloadCoroutine(float time)
    {
        _reload = true;
        yield return new WaitForSeconds(time);
        _reload = false;
    }

    private void Start()
    {
        _turrelMovement = GetComponentInParent<TurrelMovement>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject target = _turrelMovement.GetTarget();
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!target.CompareTag("Enemy"))
            {
                other.gameObject.GetComponentInParent<EnemyController>().CaughtInCrosshair();
                _turrelMovement.SetTarget(other.gameObject);
                if (_reload) return;
                Attack(other.gameObject, _bullet, _speedBullet, _lifetimeBullet);
            }
            else
            {
                if (other.gameObject == target)
                {
                    if (!_reload) return;
                    Attack(other.gameObject, _bullet, _speedBullet, _lifetimeBullet);
                }
            }
        }
    }

    public void Attack(GameObject _target, GameObject bullet, float speedBullet, float lifetimeBullet)
    {
        var damage = Random.Range(_minDamage, _maxDamage);
        var bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletInstance.GetComponent<Bullet>().InstatiateBullet(damage, _target.transform.position, speedBullet, lifetimeBullet);
        StartCoroutine(ReloadCoroutine(_reloadTime));
    }
}
