using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragToInstantiate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform rootUIPanel;
    public GameObject prefab;
    public int prefabCount = 2;
    public GameObject icon;
    public TMP_Text itemCountText;
    private RawImage rawImage;
    private Camera sceneCamera;
    private RectTransform _iconRectTransform;

    private GameObject _iconGO;

    private void Start()
    {
        _iconRectTransform = icon.GetComponent<RectTransform>();
        rootUIPanel = GameObject.Find("RootPanel").transform;
        sceneCamera = GameObject.Find("GameScene Camera").GetComponent<Camera>();
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        itemCountText.text = prefabCount.ToString();
        icon.GetComponent<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        _iconGO = Instantiate(icon);
        _iconGO.transform.SetParent(rootUIPanel);
        _iconGO.transform.position = rootUIPanel.transform.position + Vector3.right * 10000000.0f;

        icon.GetComponent<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Audio.AudioManager.Instance.PlayFX("Tap");
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
            HideIcon();
            return;
        }
        Vector3 worldPos = sceneCamera.ScreenToWorldPoint(new Vector3(localPos.x, localPos.y, 0.0f));
        worldPos.z = 0.0f;

        var hit = Physics2D.OverlapCircle(worldPos, 1.0f);
        if (hit == null)
        {
            HideIcon();
            return;
        }
        Debug.Log("Drag: Hit " + hit.transform.name);
        var unit = hit.gameObject.GetComponent<Gamelogic.Unit>();
        if (unit == null)
        {
            HideIcon();
            return;
        }

        bool success = unit.GenerateUnitComponent(prefab);
        if (success == false)
        {
            HideIcon();
            return;
        }

        itemCountText.text = (int.Parse(itemCountText.text) - 1).ToString();

        _iconGO.transform.position = rootUIPanel.transform.position + Vector3.right * 10000000.0f;

        Audio.AudioManager.Instance.PlayFX("Tap");

        if (int.Parse(itemCountText.text) <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void HideIcon()
    {
        _iconGO.transform.position = rootUIPanel.transform.position + Vector3.right * 10000000.0f;
    }
}