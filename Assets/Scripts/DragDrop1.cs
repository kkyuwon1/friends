using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop1 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 offeset;
    public string destinationing = "DropArea1";

    // Start is called before the first frame update

    void OnMouseDown()
    {
        offeset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offeset;
    }

    private void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationing)
            {
                transform.position = hitInfo.transform.position;
            }
        }
        transform.GetComponent<Collider>().enabled = true;

    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}