using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Gamelogic
{
    public class EventManager : Singleton<EventManager>
    {
        public UnityEvent onCoinCollected = new UnityEvent();

        public UnityEvent<int> onScoreChanged = new UnityEvent<int>();

        private void OnEnable()
        {
            //onCoinCollected = new UnityEvent();
            //onScoreChanged = new UnityEvent<int>();
        }
    }
}