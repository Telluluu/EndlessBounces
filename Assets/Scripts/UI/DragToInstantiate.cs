using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragToInstantiate : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Transform wholeUIPanel;
    public GameObject prefab;
    public GameObject icon;
    public TMP_Text itemCount;
    public RawImage rawImage;
    public Camera sceneCamera;
    private RectTransform _iconRectTransform;

    private GameObject _iconGO;

    private void Start()
    {
        _iconRectTransform = icon.GetComponent<RectTransform>();
        _iconGO = Instantiate(icon);
        _iconGO.transform.SetParent(wholeUIPanel);
        _iconGO.transform.position = wholeUIPanel.transform.position + Vector3.right * 10000000.0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _iconGO.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 将屏幕坐标转换为RawImage的局部坐标
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, Input.mousePosition, null, out localPos);
        Debug.Log("LocalPos = " + localPos);

        Vector2 uv = localPos / rawImage.rectTransform.sizeDelta;
        Debug.Log("UV = " + uv);
        if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
        {
            Debug.Log("Out of RawImage bounds");
            _iconGO.transform.position = wholeUIPanel.transform.position + Vector3.right * 10000000.0f;
            return;
        }
        // Vector3 ssVector = uv * rawImage.rectTransform.sizeDelta;
        Vector3 worldPos = sceneCamera.ScreenToWorldPoint(new Vector3(localPos.x, localPos.y, 0.0f));
        worldPos.z = 0.0f;
        itemCount.text = (int.Parse(itemCount.text) - 1).ToString();
        Instantiate(prefab, worldPos, Quaternion.identity);

        _iconGO.transform.position = wholeUIPanel.transform.position + Vector3.right * 10000000.0f;

        if (int.Parse(itemCount.text) <= 0)
        {
            Destroy(gameObject);
        }
    }
}