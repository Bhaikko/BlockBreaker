using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, transform.position);

        // gameObject refers to this game object
        Destroy(gameObject);
    }
}
