using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.SceneManagement;
using BlockBreaker.Player;
using System;

namespace BlockBreaker.Core {
    public class GameMode : MonoBehaviour
    {
        private float screenWidth = 0.0f;

        [SerializeField]
        Ball ballPrefab;

        [SerializeField]
        Paddle paddlePrefab;

        int blocksLeft = 0;

        private SceneLoader sceneLoader;

        private List<Ball> balls;
        private Paddle paddle;

        public bool hasStarted = false;

        void Start()
        {
            screenWidth = 2.625f * Camera.main.orthographicSize;
            
            balls = new List<Ball>();

            sceneLoader = FindObjectOfType<SceneLoader>();

            StartGame();
        }

        public void AddBlock()
        {
            blocksLeft++;
        }

        void StartGame() {
            SpawnPaddle();
            SpawnBall();
        }

        private void SpawnPaddle()
        {
            Paddle paddle = Instantiate<Paddle>(paddlePrefab);
            paddle.transform.position = new Vector2(
                screenWidth / 2.0f,
                0.25f
            );
        }

        public void BLockDestroyed()
        {
            blocksLeft--;
            if (blocksLeft <= 0) {
                // sceneLoader.LoadNextScene();
            }
        }

        public void SpawnBall() {
            Ball spawnedBall = Instantiate<Ball>(ballPrefab);
            balls.Add(spawnedBall);
        }

        public void RemovePowerUpBalls() {

        }

        public List<Ball> GetBalls() {
            return balls;
        }


    }
}
