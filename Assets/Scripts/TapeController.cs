using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeController : MonoBehaviour
{
    bool isDragging = false;
    Collider tapeCollider;
    Vector3 initialPosition;
    CasetteController casetteController;
    CasettePlayer casettePlayer;

    void Start()
    {
        tapeCollider = GetComponent<Collider>();
        initialPosition = transform.position;
        casetteController = FindObjectOfType<CasetteController>();
        casettePlayer = FindObjectOfType<CasettePlayer>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        tapeCollider.enabled = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            tapeCollider.enabled = true;

            if (casetteController.isOpen && casettePlayer.IsTapeInside(tapeCollider))
            {
                transform.position = casettePlayer.tapeInsertPosition;
            }
            else
            {
                transform.position = initialPosition;
            }
        }
    }
}
