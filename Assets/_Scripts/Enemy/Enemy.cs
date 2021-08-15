using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int enemyHealth;
    public int enemySpeed;
    public int enemyDamage;

    public void CheckHealth()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
