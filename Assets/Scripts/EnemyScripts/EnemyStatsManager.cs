using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatsManager
{
    public static EnemyStatsManager Instance;

    [Header("Combat Stats")]
    public float damage;
    public float knockbackForce;
    public float knockbackTime;

    [Header("Movement Stats")]
    public float speed;

    [Header("Health Stats")]
    public int maxHP;
}
