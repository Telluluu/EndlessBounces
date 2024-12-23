using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Gamelogic
{
    public class EventManager : Singleton<EventManager>
    {
        public UnityEvent onCoinCollected;

        private void OnDisable()
        {
            onCoinCollected?.RemoveAllListeners();
        }
    }
}