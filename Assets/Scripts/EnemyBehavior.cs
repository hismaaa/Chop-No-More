using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Transform Tree;
    [SerializeField] private float speed = 1;
     
    /// Start is called before the first frame update
    void Awake()
    {
        Tree = GameObject.FindWithTag("Tree").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (Tree.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        transform.position = Vector3.MoveTowards(transform.position, Tree.position, speed * Time.deltaTime);
    }
}
