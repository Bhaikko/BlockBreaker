using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidth = 16.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float newXPos = Input.mousePosition.x / Screen.width * screenWidth;
        newXPos = Mathf.Clamp(newXPos, 1.0f, screenWidth - 1.0f);
        Vector2 paddlePos = new Vector2(newXPos, this.transform.position.y);

        this.transform.position = paddlePos;
    }
}
