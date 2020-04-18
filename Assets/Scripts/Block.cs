using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip breakSound = null;
    [SerializeField] GameObject blockSparkles = null;
    [SerializeField] int maxHits = 1;
    [SerializeField] Sprite[] hitSprites = null;

    // Cached Reference
    Level level;
    GameStatus gameStatus;

    // State variables
    int timesHit;  // Only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();

        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            level.AddBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;

            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
        }

    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // gameObject refers to this game object
        Destroy(gameObject);
        TriggerSparkesVFX();
        level.BLockDestroyed();
    }

    private void TriggerSparkesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);    // Used to Instantiate gameObject runtime
        Destroy(sparkles, 2.0f);
    }
}
