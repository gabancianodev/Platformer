using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyChomper : Enemy {

    Rigidbody2D EnemyRigid2D;
    SpriteRenderer EnemySprite;

    [Header("Enemy Jumper Settings")]
    public float dirChangeDelay;
    public float chargeSpeed;
    public float def_chargeDelay;
    [Space]
    public AudioSource chompSound;

    float changeMovementDirTimer;
    float chargeDelay;
    
    bool enemySpotted = false;
    bool movementDirLeft = true;

    bool triggered;

    void Awake()
    {
        GameData.ENEMY_CHOMPER_DAMAGE = enemyDamage;
        changeMovementDirTimer = dirChangeDelay;
        chargeDelay = def_chargeDelay;
    }

    void Start()
    {
        EnemyRigid2D = GetComponent<Rigidbody2D>();
        EnemySprite = GetComponent<SpriteRenderer>();
    }

    void Update () {
        CheckHealth();
        Move();
	}

    void Move()
    {
        if (!enemySpotted)
        {
            changeMovementDirTimer -= Time.deltaTime;
            if (changeMovementDirTimer <= 0)
            {
                FlipSpriteDirection();
                changeMovementDirTimer = dirChangeDelay;
            }

            if (movementDirLeft)
            {
                transform.Translate(new Vector3(enemySpeed * Time.deltaTime, 0, 0));
            }
            else if (!movementDirLeft)
            {
                transform.Translate(new Vector3(-enemySpeed * Time.deltaTime, 0, 0));
            }
        }
    }

    private void FlipSpriteDirection()
    {
        if (movementDirLeft)
        {
            movementDirLeft = false;
            EnemySprite.flipX = true;
        }
        else if (!movementDirLeft)
        {
            movementDirLeft = true;
            EnemySprite.flipX = false;
        }
    }

    #region Jumper spotting the player
    void PlayerSpotted(Collider2D obj)
    {
        enemySpotted = true;
        Debug.Log("PLAYER ENETERD!");
        if (obj.gameObject.transform.position.x < transform.position.x)
        {
            EnemySprite.flipX = true;
        }
        else
        {
            EnemySprite.flipX = false;
        }
    }

    void PlayerUnSpotted(Collider2D obj)
    {
        enemySpotted = false;
        chargeDelay = def_chargeDelay;
        FlipSpriteDirection();
    }

    void Charge(Collider2D obj)
    {
        chargeDelay -= Time.deltaTime;
        if (chargeDelay <= 0)
        {
            if (obj.gameObject.transform.position.x < transform.position.x)
            {
                EnemyRigid2D.velocity = new Vector2(-chargeSpeed, EnemyRigid2D.velocity.y);
            }
            else
            {
                EnemyRigid2D.velocity = new Vector2(chargeSpeed, EnemyRigid2D.velocity.y);
            }
            chargeDelay = def_chargeDelay;
        }
    }
    #endregion

    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            chompSound.Play();
        }
    }

    #region Triggers
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            PlayerSpotted(obj);
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            Charge(obj);
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            PlayerUnSpotted(obj);
        }
       
    }
    #endregion
}
