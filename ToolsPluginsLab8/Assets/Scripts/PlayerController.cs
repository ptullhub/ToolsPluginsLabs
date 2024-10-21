using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject[] bulletPool;
    private float speed = 8f;
    private float horizontal;
    private bool canShoot = true;


    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void Shoot()
    {
        if (canShoot)
        {
            for (int i = 0; i < bulletPool.Length; i++)
            {
                if (bulletPool[i] != null)
                {
                    Bullet bullet = bulletPool[i].GetComponent<Bullet>();
                    if (bullet.canBeUsed)
                    {
                        bullet.canBeUsed = false;
                        bullet.isInUse = true;
                        bulletPool[i].transform.position = gameObject.transform.position + new Vector3(0, 1, 0);

                        canShoot = false;
                        StartCoroutine("Cooldown");

                        break;
                    }
                }
            }    
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }
}