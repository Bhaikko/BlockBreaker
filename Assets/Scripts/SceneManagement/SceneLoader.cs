using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BlockBreaker.UI;

namespace BlockBreaker.SceneManagement {
    public class SceneLoader : MonoBehaviour
    {
        public void LoadNextScene()
        {
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // Get current scene index in list of Scenes defined in Build Settings of project
            SceneManager.LoadScene((currentSceneIndex + 1) % totalScenes);
        }

        public void LoadStartScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
