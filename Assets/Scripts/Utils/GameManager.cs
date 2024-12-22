using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class GameManager : Singleton<GameManager>
    {
        public void OnGetCoin()
        {
            Debug.Log("Get Coin");
        }
    }
}