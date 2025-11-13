using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    [Header("Movement Stats")]
    public float playerSpeed;

    [Header("Projectile Stats")]
    public int bulletDamage;
    public float bulletSpeed;
    public float bulletLifetime;
    public float shootForce;
    public float shootCooldown;
    public float shootTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
