using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Gamelogic
{
    public class Launcher : MonoBehaviour
    {
        public Vector2 launchDir;
        public float launchSpeed = 30.0f;
        private bool isLaunched = false;

        public Color blinkColor;
        public float blinkDuration = 1.0f;
        public float blinkInterval = 0.1f;

        public float blinkSpeed = 1.0f;

        private Renderer _renderer;
        private Color originalColor;
        private bool blinking = false;

        private void Start()
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();
            originalColor = _renderer.material.color;
            StartBlink();
        }

        public void StartBlink()
        {
            if (blinking == false)
                StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            blinking = true;
            while (blinking)
            {
                float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);

                // 插值计算颜色
                GetComponent<Renderer>().material.color = Color.Lerp(originalColor, blinkColor, t);
                yield return new WaitForSeconds(blinkInterval);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isLaunched && Keyboard.current.spaceKey.isPressed)
            {
                var ballController = FindAnyObjectByType<BallController>();
                ballController.Launch(launchDir, launchSpeed);
                isLaunched = true;
                blinking = false;
            }
        }

        public void TryLaunch()
        {
            if (!isLaunched)
            {
                var ballController = FindAnyObjectByType<BallController>();
                ballController.Launch(launchDir, launchSpeed);
                isLaunched = true;
                blinking = false;
            }
        }
    }
}