using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.SceneManagement;
using BlockBreaker.Player;
using BlockBreaker.Environment;

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

        private int ballsCount;
        private Paddle paddle;

        public bool hasStarted = false;

        Dictionary<string, Block> blocksMapping = new Dictionary<string, Block>();

        void Start()
        {
            screenWidth = 2.625f * Camera.main.orthographicSize;
            
            ballsCount = 0;

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
            RegisterBlocks();
        }

        private void SpawnPaddle()
        {
            paddle = Instantiate<Paddle>(paddlePrefab);
            paddle.transform.position = new Vector2(
                screenWidth / 2.0f,
                0.25f
            );
        }

        public void BLockDestroyed()
        {
            blocksLeft--;
            if (blocksLeft <= 0) {
                sceneLoader.LoadNextScene();
            }
        }

        public void SpawnBall() {
            Ball spawnedBall = Instantiate<Ball>(ballPrefab);
            ballsCount++;
        }

        public void RemoveBall() {
            ballsCount--;
        }
        
        public int GetBallsCount() {
            return ballsCount;
        }

        private void RegisterBlocks() {
            Block[] blocks = FindObjectsOfType<Block>();

            foreach (Block block in blocks) {
                string key = block.transform.position.ToString();
                blocksMapping[key] = block;
            }
        }

        public Dictionary<string, Block> GetBlockMapping() {
            return blocksMapping;
        }

        public void RemoveBlockMapping(string key) {
            blocksMapping.Remove(key);
        }

        public Paddle GetPaddle() {
            return paddle;
        }
    }
}
