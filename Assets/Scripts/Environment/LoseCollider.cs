using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlockBreaker.Environment {
    public class LoseCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision) {
            SceneManager.LoadScene("Game Over"); 
            
            // Better Gameover Handle
        }
    }
}
