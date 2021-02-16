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

        private void Start() {
            Reset();
        }
        

        public void AddToScore(int score) {
            this.score += score;
        }

        public void ReduceLife() {
            this.livesLeft--;
        }

        public void Reset() {
            livesLeft = 3;
            score = 0;
        }

        public int GetLivesLeft() { return livesLeft; }
        public int GetScore() { return score; }
    }
}
