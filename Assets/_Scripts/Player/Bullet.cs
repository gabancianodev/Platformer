using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy_Blob") || obj.gameObject.CompareTag("Enemy_Chomper"))
        {
            Destroy(gameObject);
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.GetComponent<Enemy>().enemyHealth -= bulletDamage;
        }
    }
}

