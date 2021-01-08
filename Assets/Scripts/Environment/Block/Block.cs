using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;
using BlockBreaker.UI;

namespace BlockBreaker.Environment {
    public class Block : MonoBehaviour
    {
        // configuration parameters
        [SerializeField] AudioClip breakSound = null;
        [SerializeField] GameObject blockSparkles = null;

        [SerializeField] bool isBreakable = true;
        [SerializeField] int health = 1;
        [SerializeField] Sprite[] hitSprites = null;

        // Cached Reference
        GameMode gameMode;
        GameStatus gameStatus;
        SpriteRenderer spriteRenderer;

        // State variables
        int timesHit = 0; 

        private void Start()
        {
            CountBreakableBlocks();

            gameStatus = FindObjectOfType<GameStatus>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = hitSprites[timesHit];
        }

        private void CountBreakableBlocks()
        {
            gameMode = FindObjectOfType<GameMode>();

            if (isBreakable) {
                gameMode.AddBlock();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isBreakable) {
                return;
            }

            timesHit++;
            if (timesHit >= health) {
                DestroyBlock();
            } else {
                ShowNextHitSprite();
            }

        }

        private void ShowNextHitSprite()
        {
            int spriteIndex = timesHit;
            if (hitSprites[spriteIndex]) {
                spriteRenderer.sprite = hitSprites[spriteIndex];
            }
        }

        private void DestroyBlock()
        {
            // gameStatus.AddToScore();
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

            // gameObject refers to this game object
            Destroy(gameObject);
            TriggerSparkesVFX();
            gameMode.BLockDestroyed();
        }

        private void TriggerSparkesVFX()
        {
            GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);    // Used to Instantiate gameObject runtime
            Destroy(sparkles, 2.0f);
        }
    }

}
