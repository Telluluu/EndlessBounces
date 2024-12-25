using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
    public class Unit : MonoBehaviour
    {
        public Sprite normalSprite;
        public Sprite selectedSprite;

        public bool isSelected = false;
        private SpriteRenderer _sr;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            EventManager.Instance.onUnitSelected.AddListener(UnSelect);
        }

        public void Select()
        {
            EventManager.Instance.onUnitSelected.Invoke();
            isSelected = true;
            _sr.sprite = selectedSprite;
        }

        public static void UnSelectAll()
        {
            EventManager.Instance.onUnitSelected?.Invoke();
        }

        public void UnSelect()
        {
            Debug.Log("UnSelectAll");
            isSelected = false;
            _sr.sprite = normalSprite;
        }
    }
}