using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gamelogic
{
    public class Unit : MonoBehaviour
    {
        public Sprite normalSprite;
        public Sprite selectedSprite;

        public bool isSelected = false;
        public bool isPlaced = false;
        private SpriteRenderer _sr;
        public Material materialSelected;
        public Material materialUnselected;

        private GameObject _unitCom;
        private SpriteRenderer _unitComSr;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            EventManager.Instance.onUnitSelected.AddListener(UnSelect);
        }

        private void Update()
        {
            if (isSelected)
            {
                if (Keyboard.current.aKey.isPressed)
                {
                    transform.Rotate(0, 0, 1);
                }
                else if (Keyboard.current.dKey.isPressed)
                {
                    transform.Rotate(0, 0, -1);
                }
            }
        }

        public bool GenerateUnitComponent(GameObject prefab)
        {
            if (isPlaced == true)
                return false;
            isPlaced = true;
            _unitCom = Instantiate(prefab, transform.position, Quaternion.identity);
            _unitCom.transform.SetParent(transform);
            _unitCom.transform.rotation = transform.rotation;

            _sr.material = materialUnselected;
            _unitComSr = _unitCom.GetComponent<SpriteRenderer>();
            _unitComSr.material = materialUnselected;
            return true;
        }

        public void Select()
        {
            EventManager.Instance.onUnitSelected.Invoke();

            isSelected = true;
            if (isPlaced == false)
            {
                _sr.sprite = selectedSprite;
                _sr.material = materialSelected;
            }
            else
            {
                var sprite = _unitComSr.sprite;
                _unitComSr.material = materialSelected;
                _unitComSr.sprite = sprite;
            }
        }

        public static void UnSelectAll()
        {
            EventManager.Instance.onUnitSelected?.Invoke();
        }

        public void UnSelect()
        {
            isSelected = false;
            if (isPlaced == false)
            {
                _sr.sprite = normalSprite;
                _sr.material = materialUnselected;
            }
            else
                _unitComSr.material = materialUnselected;
        }
    }
}