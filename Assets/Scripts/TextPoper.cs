using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gamelogic
{
    public class TextPoper : Singleton<TextPoper>
    {
        public PopUpText popUpText;
        public PopUpText popUpTextInUI;
        public Transform ball;

        public AnimationCurve scaleCurve;  // 定义一个动画曲线
        public float scaleDuration;

        private void Start()
        {
            EventManager.Instance.onTextPoped.AddListener(FloatingText);
        }

        private void OnDestroy()
        {
            EventManager.Instance?.onTextPoped.RemoveListener(FloatingText);
        }

        public void FloatingText(string text, float size, Color color)
        {
            PopUpText go = Instantiate(popUpText, ball.position, Quaternion.identity);
            var tmp = go.gameObject.GetComponent<TextMeshPro>();
            tmp.text = text;
            tmp.color = color;
            go.StartScale(scaleCurve, scaleDuration, size);
        }

        public void FloatingText(Transform trans, string text, float size, Color color)
        {
            foreach (Transform child in trans)
            {
                Destroy(child.gameObject);
            }

            PopUpText go = Instantiate(popUpTextInUI, trans.position + Vector3.up * 2.0f, Quaternion.identity);
            go.fadeDuration = 0.5f;
            go.moveVector = Vector2.up;
            go.moveUpSpeed = 3.0f;
            go.transform.SetParent(trans);
            var tmp = go.gameObject.GetComponent<TextMeshProUGUI>();

            tmp.text = text;
            tmp.color = color;
            go.StartScale(scaleCurve, scaleDuration / 2, size);
        }
    }
}