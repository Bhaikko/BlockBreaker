using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader = null;

    int blocksLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Done using Rick's way
        //blocksLeft = FindObjectsOfType<Block>().Length;
    }

    public void AddBlock()
    {
        blocksLeft++;
    }

    public void BlockBroke()
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
