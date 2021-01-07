using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.SceneManagement;

namespace BlockBreaker.Core {
    public class GameMode : MonoBehaviour
    {
        int blocksLeft = 0;

        private SceneLoader sceneLoader;

        void Start()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
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
    }
}
