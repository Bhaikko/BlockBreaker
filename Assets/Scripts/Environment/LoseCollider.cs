using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BlockBreaker.Player;

namespace BlockBreaker.Environment {
    public class LoseCollider : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collider) {
            ActivateShield(false);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Ball>()) {
                SceneManager.LoadScene("Game Over"); 
            }
            
            // Better Gameover Handle
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
