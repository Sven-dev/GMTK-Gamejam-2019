using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    Transform Object;

    private void Start()
    {
        Object = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update ()
    {
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = 0;
        Vector3 cameraposition = Camera.main.WorldToScreenPoint(transform.position);

        mouseposition.x -= cameraposition.x;
        mouseposition.y -= cameraposition.y;

        float angle = Mathf.Atan2(mouseposition.y, mouseposition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}