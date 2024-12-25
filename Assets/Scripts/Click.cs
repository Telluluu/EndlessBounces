using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gamelogic
{
    public class Click : MonoBehaviour, IPointerClickHandler
    {
        private RawImage _rawImage;
        private Camera _sceneCamera;

        private void Start()
        {
            _rawImage = GetComponent<RawImage>();
            var cameraGO = GameObject.Find("GameScene Camera");
            if (cameraGO == null)
            {
                throw new System.Exception("GameScene Camera not found");
            }
            _sceneCamera = cameraGO.GetComponent<Camera>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // 将屏幕坐标转换为RawImage的局部坐标

            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rawImage.rectTransform, Input.mousePosition, null, out localPos);
            Debug.Log("LocalPos = " + localPos);

            Vector2 uv = localPos / _rawImage.rectTransform.sizeDelta;
            Debug.Log("UV = " + uv);
            if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
            {
                Debug.Log("out of RawImage bounds");
                return;
            }

            //Ray ray = _sceneCamera.ViewportPointToRay(uv);
            Vector3 worldPos = _sceneCamera.ScreenToWorldPoint(new Vector3(localPos.x, localPos.y, 0.0f));
            worldPos.z = 0.0f;
            var hits = Physics2D.OverlapCircleAll(worldPos, 1.0f);
            bool hitAnyUnit = false;
            foreach (var hit in hits)
            {
                if (hitAnyUnit)
                    break;

                if (hit != null)
                {
                    Debug.Log("Hit " + hit.transform.name);
                    var unit = hit.gameObject.GetComponent<Gamelogic.Unit>();
                    if (unit != null)
                    {
                        unit.Select();
                        hitAnyUnit = true;
                    }
                }
            }
            if (hitAnyUnit == false)
            {
                Debug.Log("No Hit");
                Gamelogic.Unit.UnSelectAll();
            }
        }
    }
}