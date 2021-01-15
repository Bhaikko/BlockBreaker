﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;
using BlockBreaker.Player;
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
        PickupHandler pickupHandler;

        Paddle paddle;

        // State variables
        int timesHit = 0; 

        private void Start()
        {
            CountBreakableBlocks();

            gameStatus = FindObjectOfType<GameStatus>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            pickupHandler = GetComponent<PickupHandler>();

            paddle = FindObjectOfType<Paddle>();

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
            Dictionary<Powerup, bool> activePowerups = paddle.GetActivePowerups();

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

            // gameObject refers to this game object
            Destroy(gameObject);
            TriggerSparkesVFX();

            if (isBreakable) {
                gameMode.BLockDestroyed();
            }
        }

        private void TriggerSparkesVFX()
        {
            GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);    // Used to Instantiate gameObject runtime
            Destroy(sparkles, 2.0f);
        }
    }

}