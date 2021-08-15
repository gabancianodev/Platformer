using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    SpriteRenderer BulletRenderer;
    Rigidbody2D BulletRigid2D;

    [Header("Weapon Settings")]
    public GameObject Bullet;
    public Transform FirePoint;
    [Space]
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletFireOffsetX;
    [Space]

    GameObject currentBullet;
    Vector2 FirePointPos;

    public void Shoot(bool playerAxisX)
    {
        currentBullet = Instantiate(Bullet, GetBulletSpawnPosition(playerAxisX), transform.rotation);
        BulletRenderer = currentBullet.GetComponent<SpriteRenderer>();
        BulletRigid2D = currentBullet.GetComponent<Rigidbody2D>();

        if (playerAxisX)
        {
            BulletRenderer.flipX = true;
            BulletRigid2D.velocity = new Vector2(-bulletSpeed, BulletRigid2D.velocity.y);
        }
        else if (!playerAxisX)
        {
            BulletRenderer.flipX = false;
            BulletRigid2D.velocity = new Vector2(bulletSpeed, BulletRigid2D.velocity.y);
        }
        StartCoroutine(DestroyBullet(currentBullet));
    }

    Vector2 GetBulletSpawnPosition(bool playerAxisX)
    {
        if (playerAxisX)
        {
            FirePointPos = new Vector2(FirePoint.position.x + -bulletFireOffsetX, FirePoint.position.y);
        }
        else if (!playerAxisX)
        {
            FirePointPos = new Vector2(FirePoint.position.x + bulletFireOffsetX, FirePoint.position.y);
        }
        return FirePointPos;
    }

    IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet);
    }
}
