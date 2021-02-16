using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BlockBreaker.Player;
using BlockBreaker.Core;

namespace BlockBreaker.Environment {
    public class LoseCollider : MonoBehaviour
    {
        PlayerStatsHandler playerStats = null;

        GameMode gameMode;

        private void Start() {
            gameMode = FindObjectOfType<GameMode>();
            playerStats = FindObjectOfType<PlayerStatsHandler>();
        }

        private void OnCollisionEnter2D(Collision2D collider) {
            ActivateShield(false);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Ball>()) {
                if (gameMode.GetBallsCount() <= 1) {
                    playerStats.ReduceLife();

                    if (playerStats.GetLivesLeft() <= 0) {
                        // Destroy(playerStats);
                        SceneManager.LoadScene("Game Over"); 
                    } else {
                        gameMode.hasStarted = false;
                        gameMode.SpawnBall();

                    }
                }
                
                Destroy(collision.gameObject);
                gameMode.RemoveBall();

            }            
        }

        public void ActivateShield() {
            ActivateShield(true);
        }

        private void ActivateShield(bool value) {
            Transform shield = transform.GetChild(0);

            GetComponent<BoxCollider2D>().isTrigger = !value;
            shield.gameObject.SetActive(value);
        }
    }
}
