using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BlockBreaker.Core;

namespace BlockBreaker.Player {
    public class Paddle : MonoBehaviour
    {
        GameMode gameMode;

        void Start() {
            gameMode = FindObjectOfType<GameMode>();
        }

        public void ChangeSize(float sizeX = 1.0f) {
            transform.localScale = new Vector2(sizeX, 1.0f);
        }
    }

}
