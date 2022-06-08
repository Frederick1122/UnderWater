using System.Collections;
using UnityEngine;

public class DeathBullet : MonoBehaviour
{
    [SerializeField]private AnimationClip animationClip;
    void Start()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(animationClip.length);
        Destroy(gameObject);
    }
}
