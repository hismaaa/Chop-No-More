using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, PlayerStatsManager.Instance.bulletLifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * PlayerStatsManager.Instance.bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();

            if (enemy != null)
            {
                enemy.TakeDamage(PlayerStatsManager.Instance.bulletDamage);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
