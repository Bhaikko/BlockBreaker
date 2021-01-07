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

        //[SerializeField] int maxHits = 1;
        [SerializeField] Sprite[] hitSprites = null;

        // Cached Reference
        GameMode gameMode;
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
            gameMode = FindObjectOfType<GameMode>();

            if (tag == "Breakable") {
                gameMode.AddBlock();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (tag == "Breakable") {
                timesHit++;
                int maxHits = hitSprites.Length + 1;
                if (timesHit >= maxHits) {
                    DestroyBlock();
                } else {
                    ShowNextHitSprite();
                }
            }

        }

        private void ShowNextHitSprite()
        {
            int spriteIndex = timesHit - 1;
            if (hitSprites[spriteIndex]) {
                GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            }
        }

        private void DestroyBlock()
        {
            gameStatus.AddToScore();
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
