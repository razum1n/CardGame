using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    public bool usePointerDisplacement = true;

    private bool dragging = false;

    private Vector3 pointerDisplacement = Vector3.zero;

    private float zDisplacement;

    private void OnMouseDown()
    {
        dragging = true;
        zDisplacement = -Camera.main.transform.position.z + transform.position.z;
        if (usePointerDisplacement)
            pointerDisplacement = -transform.position + MouseInWorldCords();
        else
            pointerDisplacement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if(dragging)
        {
            Vector3 mousePos = MouseInWorldCords();
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
        }
        
    }

    private void OnMouseUp()
    {
        if(dragging)
        {
            dragging = false;
        }
    }

    private Vector3 MouseInWorldCords()
    {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
