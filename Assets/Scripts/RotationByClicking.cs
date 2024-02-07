using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByClicking : MonoBehaviour
{
    Vector3 StartPosition, NewPosition;

    void Update()
    {
        if (!GameManager.instance.Rotate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                NewPosition = StartPosition - Input.mousePosition;
                this.gameObject.transform.Rotate(new Vector3(0, NewPosition.x * 0.5f, 0));
                StartPosition = Input.mousePosition;
            }
        }
    }
}
