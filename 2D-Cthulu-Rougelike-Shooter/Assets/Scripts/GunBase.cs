using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GunBase : NetworkBehaviour
{
    public GunSO gunStats;
    public bool shouldLoadNewStats;
    public float range = 10;
    public CircleCollider2D col;
    public GameObject target;
    public float fireRate;
    public float timer;
    public float damage;
    public GameObject bulletPrefab;
    public List<GameObject> withinBounds;

    // Start is called before the first frame update
    private void Start()
    {
        col.radius = range;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!HasStateAuthority) return;
        if(FindObjectOfType<Timer>().Frozen) return;

        // LookAtTarget();
        SetTarget();

        timer += Time.deltaTime;
        if(gunStats != null)
        {
            if(timer >= fireRate && target != null)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    public override void Render()
    {
        if(HasStateAuthority)
        {
            if(shouldLoadNewStats)
            {
                LoadNewStats();
            }
            else
            {
                LookAtTarget();
            }
        }
        else
        {
            if(transform.parent.localScale.x == 1)
            {
                // transform.localScale = new(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(transform.parent.localScale.x == -1)
            {
                // transform.localScale = new(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void AsssignGunStats(GunSO stats)
    {
        gunStats = stats;
        shouldLoadNewStats = true;
    }

    private void LoadNewStats()
    {
        if(gunStats == null)
        {
            Debug.Log("No gunstats assigned");
            return;
        }

        GetComponent<SpriteRenderer>().sprite = gunStats.gunSprite;
        range = gunStats.range;
        fireRate = gunStats.fireRate;
        damage = gunStats.damage;

        shouldLoadNewStats = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!HasStateAuthority) return;

        if(other.CompareTag("Enemy"))
        {
            withinBounds.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!HasStateAuthority) return;

        if(other.CompareTag("Enemy"))
        {
            withinBounds.Remove(other.gameObject);
        }
    }

    public void SetTarget()
    {
        List<GameObject> toRemove = new List<GameObject>();

        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject col in withinBounds)
        {
            if (col == null || !col.CompareTag("Enemy"))
            {
                toRemove.Add(col);
                continue;
            }

            float distanceToEnemy = Vector2.Distance(transform.position, col.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = col;
            }
        }

        foreach (GameObject obj in toRemove)
        {
            withinBounds.Remove(obj);
        }

        target = nearestEnemy;
    }


    private void LookAtTarget()
    {
        if(target != null)
        {
            Debug.Log("Valid Target");
            if(transform.parent.localScale.x == 1)
            {
                // transform.localScale = new(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(transform.parent.localScale.x == -1)
            {
                // transform.localScale = new(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<SpriteRenderer>().flipX = true;
            }
                
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Shoot()
    {
        // Shoot in the direction of the target
        Vector2 direction = (target.transform.position - transform.position).normalized;
        NetworkObject bullet = Runner.Spawn(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
        bullet.GetComponent<Bullet>().SetDamage(damage);
        // Assuming bulletSpeed is defined somewhere
    }
}
