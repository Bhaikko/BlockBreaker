using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BlockBreaker.Player;

namespace BlockBreaker.Environment {
    public class LoseCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Ball>()) {
                SceneManager.LoadScene("Game Over"); 
            }
            
            // Better Gameover Handle
        }
    }
}
