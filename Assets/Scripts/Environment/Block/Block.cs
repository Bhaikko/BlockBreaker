using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;
using BlockBreaker.UI;
using BlockBreaker.Player;
using System.Linq;

namespace BlockBreaker.Environment {
    public class Block : MonoBehaviour
    {
        [SerializeField] BlockType blockType = BlockType.DEFAULT;

        // configuration parameters
        [SerializeField] AudioClip breakSound = null;
        [SerializeField] GameObject blockSparkles = null;

        [SerializeField] bool isBreakable = true;
        [SerializeField] int health = 1;
        [SerializeField] Sprite[] hitSprites = null;
        [SerializeField] int score = 10;


        // Cached Reference
        GameMode gameMode;
        GameStatus gameStatus;
        SpriteRenderer spriteRenderer;
        PickupHandler pickupHandler;

        PowerupHandler powerupHandler;

        // State variables
        int timesHit = 0; 

        private void Start()
        {
            CountBreakableBlocks();

            gameStatus = FindObjectOfType<GameStatus>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            pickupHandler = GetComponent<PickupHandler>();

            powerupHandler = FindObjectOfType<PowerupHandler>();

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
            if (collision.gameObject.GetComponent<Ball>()) {
                HandleBlockCollision();

            } 
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if (
                collider.gameObject.GetComponent<Ball>() || 
                collider.gameObject.GetComponent<Laser>()
            ) { 
                HandleBlockCollision();
            }
        }

        public void HandleBlockCollision() {
            Dictionary<Powerup, bool> activePowerups = powerupHandler.GetActivePowerups();

            if (blockType == BlockType.EXPLOSION) {
                HandleExplosionBlock();
            } else if (blockType == BlockType.LINE_CLEAR) {
                Debug.Log("Clearing Line");
                HandlerLineClear();
            }

            if (activePowerups[Powerup.ONE_HIT_KILL]) {
                DestroyBlock();
                return;
            }


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

            // Try Spawning A Pickup
            if (pickupHandler) {
                pickupHandler.TrySpawningPickup();
            } else {
                Debug.Log("Pickup Handler Not Attached.");
            }

            gameMode.RemoveBlockMapping(transform.position.ToString());

            FindObjectOfType<PlayerStatsHandler>().AddToScore(score);

            // gameObject refers to this game object
            Destroy(gameObject);
            TriggerSparkesVFX();

            if (isBreakable) {
                gameMode.BLockDestroyed();
            }
        }

        private void TriggerSparkesVFX()
        {
            GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);
            Destroy(sparkles, 2.0f);
        }

        private void HandleExplosionBlock() {
            Vector3 currentPos = transform.position;

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if (x == 0 && y == 0) {
                        continue;
                    }
                    Vector3 newVector = currentPos + new Vector3(x, y, 0.0f);
                    if (gameMode.GetBlockMapping().ContainsKey(newVector.ToString())) {
                        gameMode.GetBlockMapping()[newVector.ToString()].DestroyBlock();
                    }
                }
            }
        }

        private void HandlerLineClear() {
            Vector3 currentPos = transform.position;

            DestroyInDirection(currentPos, 1.0f, 0.0f);
            DestroyInDirection(currentPos, -1.0f, 0.0f);

            DestroyBlock();

        }

        private void DestroyInDirection(Vector3 currentPos, float directionX = 0.0f, float directionY = 0.0f) {
            Vector3 newVector = currentPos;
            while (true) {
                newVector = newVector + new Vector3(directionX, directionY, 0.0f);

                if (gameMode.GetBlockMapping().ContainsKey(newVector.ToString())) {
                    gameMode.GetBlockMapping()[newVector.ToString()].DestroyBlock();
                } else {
                    break;
                }
            }
        }
    }

}
