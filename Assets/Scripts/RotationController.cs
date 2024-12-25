using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationController : MonoBehaviour
{
    private void Update()
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