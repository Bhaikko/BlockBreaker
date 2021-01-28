using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;

namespace BlockBreaker.Player {
    public class Paddle : MonoBehaviour
    {
        GameMode gameMode;

        [SerializeField] LaserSpawner[] spawners;

        void Start() {
            gameMode = FindObjectOfType<GameMode>();

            foreach (LaserSpawner spawner in spawners) {
                spawner.gameObject.SetActive(false);
            }
        }

        public void ChangeSize(float sizeX = 1.0f) {
            transform.localScale = new Vector2(sizeX, 1.0f);
        }

        public void ActivateLasers() {
            foreach (LaserSpawner spawner in spawners) {
                spawner.gameObject.SetActive(true);
                spawner.StartShooting();
            }
        }

        public void DeactivateLasers() {
            foreach (LaserSpawner spawner in spawners) {
                spawner.gameObject.SetActive(false);
                spawner.StopShooting();
            }
        }

    }

}
