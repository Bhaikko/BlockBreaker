using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Core {
    public class PlayerStatsHandler : MonoBehaviour
    {
        [SerializeField] int livesLeft = 3;
        [SerializeField] int score = 0;

        private void Awake() {
            int playerStatsCount = FindObjectsOfType<PlayerStatsHandler>().Length;
            if (playerStatsCount > 1) {
                gameObject.SetActive(false);
                Destroy(gameObject);
            } else {
                DontDestroyOnLoad(gameObject);
            }
        }
        

        public void AddToScore(int score) {
            this.score += score;
        }

        public void ReduceLife() {
            this.livesLeft--;
        }

        public int GetLivesLeft() { return livesLeft; }
        public int GetScore() { return score; }
    }
}
