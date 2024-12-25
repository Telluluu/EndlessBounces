using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
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