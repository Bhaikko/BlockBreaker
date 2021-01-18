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
    }

}
