using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockBreaker.Player {
    public class Paddle : MonoBehaviour
    {
        // [SerializeField] float screenWidth = 16.0f;
        private float screenWidth = 0.0f;

        void Start() {
            screenWidth = 2.625f * Camera.main.orthographicSize;
        }

        // Update is called once per frame
        void Update()
        {
            float newXPos = Input.mousePosition.x / Screen.width * screenWidth;
            //float newXPos = Input.mousePosition.x;
            newXPos = Mathf.Clamp(newXPos, 1.0f, screenWidth - 1.0f);
            Vector2 paddlePos = new Vector2(newXPos, this.transform.position.y);

            this.transform.position = paddlePos;
        }
    }
}
