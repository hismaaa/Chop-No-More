using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private EnemyStatsManager enemyStats;
    Transform Tree;
    private Rigidbody _rb;
    private int _currentHP;
    private bool _isKnockedBack = false;

    /// Start is called before the first frame update
    void Awake()
    {
        Tree = GameObject.FindWithTag("Tree").transform;
        _rb = GetComponent<Rigidbody>();

        _currentHP = enemyStats.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isKnockedBack)
        {
            MoveTowardsTree();
        }
    }

    void MoveTowardsTree()
    {
        if (Tree == null) return;

        Vector3 direction = (Tree.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        transform.position = Vector3.MoveTowards(transform.position, Tree.position, enemyStats.speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyKnockback(Vector3 direction)
    {
        if (_rb != null)
        {
            StartCoroutine(KnockbackCoroutine(direction));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector3 direction)
    {
        _isKnockedBack = true;

        _rb.AddForce(direction.normalized * enemyStats.knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(enemyStats.knockbackTime);

        _isKnockedBack = false;

        _rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (!_isKnockedBack)
            {
                PerformAttack(collision.gameObject);
            }
        }
    }

    private void PerformAttack(GameObject target)
    {
        float damage = enemyStats.damage;

        // TODO: Create Tree Stats Manager
        // TreeHealth treeHealth = target.GetComponent<TreeHealth>();
        // if (treeHealth != null) treeHealth.TakeDamage(damage);

        Vector3 knockbackDirection = (transform.position - target.transform.position).normalized;
        ApplySelfKnockback(knockbackDirection);
    }

    private void ApplySelfKnockback(Vector3 direction)
    {
        if (_rb != null)
        {
            StartCoroutine(SelfKnockbackCoroutine(direction));
        }
    }

    private IEnumerator SelfKnockbackCoroutine(Vector3 direction)
    {
        _isKnockedBack = true;

        _rb.AddForce(direction.normalized * enemyStats.knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(enemyStats.knockbackTime);

        _isKnockedBack = false;

        _rb.velocity = Vector3.zero;
    }

    public float GetHealthPercentage()
    {
        return (float)_currentHP / enemyStats.maxHP;
    }
}
