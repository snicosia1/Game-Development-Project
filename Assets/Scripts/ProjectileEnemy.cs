using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectileEnemy : MonoBehaviour
{
    [Header("Projectile Settings")]
    public Transform projectilePrefab; // Your 2D projectile (Sprite, etc.)
    public Transform firePoint;       // Where projectiles spawn
    public Transform targetPoint;    // Where projectiles disappear
    public float fireRate = 2f;
    public float projectileSpeed = 5f;

    [Header("2D Settings")]
    public bool faceRight = true;    // Flip sprite direction
    private List<Transform> activeProjectiles = new List<Transform>();
   

    private float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time + 1f / fireRate;
       
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireProjectile()
    {
        if (!projectilePrefab || !firePoint || !targetPoint) return;

        Transform projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        activeProjectiles.Add(projectile);
        StartCoroutine(MoveProjectile(projectile));
    }

    IEnumerator MoveProjectile(Transform projectile)
    {
        while (projectile != null)
        {
            // 2D movement using Vector2
            projectile.position = Vector2.MoveTowards(
                projectile.position,
                targetPoint.position,
                projectileSpeed * Time.deltaTime
            );

            // Check if reached target (with 2D distance check)
            if (Vector2.Distance(projectile.position, targetPoint.position) < 0.05f)
            {
                Destroy(projectile.gameObject);
                yield break;
            }

            yield return null;
        }
    }

    void CleanupProjectiles()
    {
        foreach (Transform projectile in activeProjectiles)
        {
            if (projectile != null)
            {
                Destroy(projectile.gameObject);
            }
        }
        activeProjectiles.Clear();
    }

    public void DefeatEnemy()
    {
        CleanupProjectiles();
        Destroy(gameObject); 

    }
}
