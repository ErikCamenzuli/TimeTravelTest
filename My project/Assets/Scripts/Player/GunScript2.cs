using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript2 : MonoBehaviour
{
    private float nextFireTime;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private Transform firePosition;
    [SerializeField] private bool projectilGarv = true;
    [SerializeField] private float rangeProjetTileSpeed = 75f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int damage = 10;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(projectile, firePosition.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (projectilGarv)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }

        rb.AddForce(gameObject.transform.forward * rangeProjetTileSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);

    }
}
