using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.SceneManagement;
using BlockBreaker.Player;

namespace BlockBreaker.Core {
    public class GameMode : MonoBehaviour
    {
        [SerializeField]
        Ball ballPrefab;

        int blocksLeft = 0;

        private SceneLoader sceneLoader;

        private List<Ball> balls;

        void Start()
        {
            balls = new List<Ball>();

            sceneLoader = FindObjectOfType<SceneLoader>();
            SpawnBall();
        }

        public void AddBlock()
        {
            blocksLeft++;
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
            balls.Add(spawnedBall);
        }

        public void RemovePowerUpBalls() {

        }


    }
}
