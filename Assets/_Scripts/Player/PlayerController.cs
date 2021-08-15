using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : Weapon {
    
    public SFXManager sfx;
    [Header("Player Settings")]
    public int def_playerHealth;
    public int def_playerAmmo;
    [Space]
    public float movementSpeed;
    public float jumpForce;
    public float hitDisableTime;
    [Space]
    public AudioSource GunEmpty;

    private bool isGrounded;
    private bool isShooting;

    private bool m_leftButtonPressed;
    private bool m_rightButtonPressed;

    SpriteRenderer PlayerSprite;
    Rigidbody2D PlayerRigid2D;
    Animator PlayerAnimator;

    Vector3 SpawnPosition;

    GameObject CoinTemp;

    bool t_healthpowerup;
    bool t_ammopowerup;
    bool t_coin;

    void Awake()
    {
        GameData.playerHealth = def_playerHealth;
        GameData.playerAmmo = def_playerAmmo;
        GameData.playerCoin = 0;
        Debug.Log(GameData.playerHealth);

        GameData.isPlayerAlive = true;

        GameData.tc_pHealth = true;
        GameData.tc_pAmmo = true;
        GameData.tc_coin = true;

        SpawnPosition = transform.localPosition;
        isShooting = false;
    }

    void Start() {
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerRigid2D = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }
    
    void Update() {
        DetectUserInput();
        UpdateMobileMovements();
    }

    #region Game Controls
    void DetectUserInput()
    {
        if (GameData.isPlayerAlive)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeftPressed();
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveRightPressed();
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                MovementReleased();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ShootPressed();
            }
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                ShootReleased();
            }
        }
    }

    public void MoveLeftPressed()
    {
        if (!isShooting)
        {
            PlayerRigid2D.velocity = new Vector2(-movementSpeed, PlayerRigid2D.velocity.y);
            PlayerSprite.flipX = true;
            PlayerAnimator.SetBool("isRunning", true);
        }
    }

    public void MoveRightPressed()
    {
        if (!isShooting)
        {
            PlayerRigid2D.velocity = new Vector2(movementSpeed, PlayerRigid2D.velocity.y);
            PlayerSprite.flipX = false;
            PlayerAnimator.SetBool("isRunning", true);
        }
    }
    public void MovementReleased()
    {
        PlayerRigid2D.velocity = new Vector2(0, PlayerRigid2D.velocity.y);

        PlayerAnimator.SetBool("isRunning", false);

        m_leftButtonPressed = false;
        m_rightButtonPressed = false;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isShooting = false;
            PlayerRigid2D.velocity = new Vector2(PlayerRigid2D.velocity.x, jumpForce);
            sfx.player_jump.Play();
        }
    }

    public void ShootPressed()
    {
        if (GameData.playerAmmo > 0)
        {
            if (isGrounded)
            {
                Shoot(PlayerSprite.flipX);
                GameData.playerAmmo--;
                isShooting = true; 
                PlayerAnimator.SetBool("isShooting", isShooting);
                PlayerRigid2D.velocity = Vector2.zero;
                sfx.player_shoot.Play();
            }
        }
        else
        {
            GunEmpty.Play();
        }
    }
    public void ShootReleased()
    {
        if (GameData.playerAmmo > 0)
        {
            if (isGrounded)
            {
                PlayerRigid2D.velocity = Vector2.zero;
            }
        }
        isShooting = false;
        PlayerAnimator.SetBool("isShooting", isShooting);
    }
    #endregion

    #region Mobile Version Controls
    void UpdateMobileMovements()
    {
        if (m_leftButtonPressed)
        {
            m_rightButtonPressed = false;
            MoveLeftPressed();
        }
        if (m_rightButtonPressed)
        {
            m_leftButtonPressed = false;
            MoveRightPressed();
        }
    }

    public void LeftButtonPressed()
    {
        m_leftButtonPressed = true;
    }

    public void RightButtonPressed()
    {
        m_rightButtonPressed = true;
    }
    #endregion

    void CheckPlayerHealth()
    {
        if(GameData.playerHealth <= 0)
        {
            SceneManager.LoadScene("gameover");
        }
        else
        {
            StartCoroutine(DisablePlayerTemporarily(hitDisableTime));
        }
    }

    IEnumerator DisablePlayerTemporarily(float disableTime)
    {
        gameObject.layer = 10;
        float oneThirdofTime = disableTime / 8;
        PlayerSprite.enabled = false;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = true;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = false;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = true;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = false;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = true;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = false;
        yield return new WaitForSeconds(oneThirdofTime);
        PlayerSprite.enabled = true;
        gameObject.layer = 8;
    }

    #region Collisions
    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Enemy_Blob"))
        {
            PlayerAnimator.SetBool("isPlayerAlive", GameData.isPlayerAlive);
            sfx.player_hit.Play();
            GameData.playerHealth -= GameData.ENEMY_BLOB_DAMAGE;
            CheckPlayerHealth();
        }
        if (obj.gameObject.CompareTag("Enemy_Chomper"))
        {
            PlayerAnimator.SetBool("isPlayerAlive", GameData.isPlayerAlive);
            sfx.player_hit.Play();
            GameData.playerHealth -= GameData.ENEMY_CHOMPER_DAMAGE;
            CheckPlayerHealth();
        }
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            PlayerRigid2D.velocity = Vector2.zero;
            PlayerAnimator.SetBool("isJumping", false);
        }

        if (obj.gameObject.CompareTag("PowerUp_Health"))
        {
            if (GameData.tc_pHealth)
            {
                if (GameData.playerHealth < def_playerHealth)
                {
                    sfx.player_pickup_health.Play();
                    Destroy(obj.gameObject);
                    GameData.playerHealth += GameData.F_HEALTHKIT_INCREASE;
                    GameData.tc_pHealth = false;
                }
            }
        }

        if (obj.gameObject.CompareTag("PowerUp_Ammo"))
        {
            if (GameData.tc_pAmmo)
            {
                if (GameData.playerAmmo < def_playerAmmo)
                {
                    sfx.player_pickup_health.Play();
                    Destroy(obj.gameObject);
                    GameData.playerAmmo += GameData.F_AMMOKIT_INCREASE;
                    if (GameData.playerAmmo >= def_playerAmmo)
                    {
                        GameData.playerAmmo = def_playerAmmo;
                    }
                    GameData.tc_pAmmo = false;
                }
            }
            t_ammopowerup = true;
        }

        if (obj.gameObject.CompareTag("Coin"))
        {
            if (GameData.tc_coin)
            {
                sfx.player_pickup_coin.Play();
                GameData.playerCoin++;
                Destroy(obj.gameObject);
                GameData.tc_coin = false;
            }
        }

        if (obj.gameObject.CompareTag("FallDeath"))
        {
            sfx.player_hit.Play();
            PlayerAnimator.SetBool("isPlayerAlive", GameData.isPlayerAlive);
            GameData.playerHealth -= GameData.F_FALL_DAMAGE;
            transform.localPosition = SpawnPosition;
            PlayerRigid2D.velocity = Vector2.zero;
            CheckPlayerHealth();
        }

        if (obj.gameObject.CompareTag("EndLevelEgg"))
        {
            Destroy(obj.gameObject);
            SceneManager.LoadScene("levelsuccess");
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            PlayerAnimator.SetBool("isRunning", false);
            PlayerAnimator.SetBool("isShooting", false);
            PlayerAnimator.SetBool("isJumping", true);
        }
    }
    #endregion
}
