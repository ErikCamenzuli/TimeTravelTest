using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GunScript : MonoBehaviour
{
    [SerializeField] private float hitScanRange = 500f;
    [SerializeField] private LayerMask hitScanLayer;

    [SerializeField] private Transform firePosition;
    [SerializeField] private int damage = 10;

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            HitScanShoot();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * hitScanRange);
    }

    private void HitScanShoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, hitScanRange, hitScanLayer))
        {
            Debug.Log(hit.collider.gameObject.name);

            EnemyAI enemy = hit.collider.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
