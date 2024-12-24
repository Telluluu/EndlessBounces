using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gamelogic
{
    public class TextPoper : MonoBehaviour
    {
        public Transform popUpText;
        public Transform ball;

        private void Start()
        {
            EventManager.Instance.onTextPoped.AddListener(FloatintText);
        }

        private void OnDestroy()
        {
            EventManager.Instance?.onTextPoped.RemoveListener(FloatintText);
        }

        public void FloatintText(string text, float size, Color color)
        {
            var go = Instantiate(popUpText, ball.position, Quaternion.identity);
            var tmp = go.gameObject.GetComponent<TextMeshPro>();
            tmp.text = text;
            tmp.color = color;

            go.transform.localScale = go.transform.localScale * size;
        }
    }
}