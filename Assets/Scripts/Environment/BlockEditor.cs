using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[ExecuteInEditMode]
[RequireComponent(typeof(BlockBreaker.Environment.Block))]
public class BlockEditor : MonoBehaviour
{
    private float screenWidth = 0.0f;

    void Start() {
        screenWidth = 2.625f * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            Mathf.Round(transform.position.x),  
            Mathf.Round(transform.position.y)  
        );
    }
}
