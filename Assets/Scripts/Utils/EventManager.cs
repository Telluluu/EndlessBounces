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

        // 飘字文本，字体大小（倍率），颜色
        public UnityEvent<string, float, Color> onTextPoped = new UnityEvent<string, float, Color>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}