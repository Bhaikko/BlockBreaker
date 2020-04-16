using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound = null;

    // Cached Reference
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.AddBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // gameObject refers to this game object
        Destroy(gameObject);
        level.BlockBroke();
        
    }
}
