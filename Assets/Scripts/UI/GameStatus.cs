using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlockBreaker.UI {
    public class GameStatus : MonoBehaviour
    {
        [Range(0.1f, 10.0f)]
        [SerializeField] float speed = 1.0f;
        [SerializeField] int pointsPerBlockDestroyed = 83;

        [SerializeField] Text playerScore = null;

        // State Variables
        [SerializeField] int currentScore = 0;

        private void Awake()
        {
            // Implementing Singleton Pattern
            int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
            if (gameStatusCount > 1)
            {   
                // TO fix null exceptions in Singleton as there is some delay between Awake() and Destroy();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            playerScore.text = currentScore.ToString();
        }

        // Update is called once per frame
        void Update()
        {

            // Change Game Speed
            Time.timeScale = speed;
        }

        public void AddToScore()
        {
            currentScore += pointsPerBlockDestroyed;
            playerScore.text = currentScore.ToString();
        }

        public void ResetGameStatus()
        {
            Destroy(gameObject);
        }
    }
}

