using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;              // your main camera
    public GameObject bulletPrefab; // bullet prefab
    public Transform shootPoint;    // where bullets come from

    void Update()
    {
        if (Time.time >= PlayerStatsManager.Instance.shootTime)
        {
            Shoot();
            PlayerStatsManager.Instance.shootTime = Time.time + PlayerStatsManager.Instance.shootCooldown;
        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, 100f))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        // Flatten the target Y to keep bullet roughly horizontal
        Vector3 direction = (targetPoint - shootPoint.position).normalized;
        direction.y = 0;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(direction * PlayerStatsManager.Instance.shootForce, ForceMode.Impulse);
    }
}