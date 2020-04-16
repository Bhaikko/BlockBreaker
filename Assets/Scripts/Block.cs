using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // gameObject refers to this game object
        Destroy(gameObject, 2);
    }
}
