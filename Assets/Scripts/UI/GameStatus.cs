using System.Collections;
using System.Collections.Generic;
using BlockBreaker.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BlockBreaker.UI {
    public class GameStatus : MonoBehaviour
    {
        [SerializeField] Text playerScore = null;
        [SerializeField] Text livesText = null;

        PlayerStatsHandler playerStatsHandler;

        private void Start()
        {
            playerStatsHandler = FindObjectOfType<PlayerStatsHandler>();

            playerScore.text = playerStatsHandler.GetScore().ToString();
            livesText.text = playerStatsHandler.GetLivesLeft().ToString();
        }

        void Update()
        {
            playerScore.text = playerStatsHandler.GetScore().ToString();
            livesText.text = playerStatsHandler.GetLivesLeft().ToString();

        }

    }
}

