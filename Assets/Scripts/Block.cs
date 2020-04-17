using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound = null;
    [SerializeField] GameObject blockSparkles = null;

    // Cached Reference
    Level level;
    GameStatus gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.AddBlock();

        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();

    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // gameObject refers to this game object
        Destroy(gameObject);
        TriggerSparkesVFX();
        level.BLockDestroyed();
    }

    private void TriggerSparkesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);    // Used to Instantiate gameObject runtime
        Destroy(sparkles, 2.0f);
    }
}
