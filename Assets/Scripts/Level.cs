using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int blocksLeft = 0;

    SceneLoader sceneLoader;

    void Start()
    {
        // Done using Rick's way
        //blocksLeft = FindObjectsOfType<Block>().Length;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBlock()
    {
        blocksLeft++;
    }

    public void BLockDestroyed()
    {
        blocksLeft--;
        if (blocksLeft <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
