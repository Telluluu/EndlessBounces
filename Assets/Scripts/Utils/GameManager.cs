using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class GameManager : Singleton<GameManager>
    {
        public int getCoinCount = 0;
        public int maxCoinCount = 3;
    }
}