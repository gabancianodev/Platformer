using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlob : Enemy
{
    [Header("Blob Settings")]
    public float blobStepDelay;

    SpriteRenderer BlobSprite;
    Rigidbody2D BlobRigid2D;

    private void Awake()
    {
        GameData.ENEMY_BLOB_DAMAGE = enemyDamage;   
    }

    void Start()
    {
        BlobSprite = GetComponent<SpriteRenderer>();
        BlobRigid2D = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveLeft());
    }

    void Update()
    {
        CheckHealth();
    }

    IEnumerator MoveLeft()
    {
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);

        BlobSprite.flipX = true;

        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(-enemySpeed, BlobRigid2D.velocity.y);
        StartCoroutine(MoveRight());
    }

    IEnumerator MoveRight()
    {
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);

        BlobSprite.flipX = false;
        
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);
        yield return new WaitForSeconds(blobStepDelay);
        BlobRigid2D.velocity = new Vector2(enemySpeed, BlobRigid2D.velocity.y);
        StartCoroutine(MoveLeft());
    }
}