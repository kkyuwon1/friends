using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    bool isDragging = false;
    bool animationTriggered = false;
    Collider boxCollider;
    Animator animator;
    Vector3 initialPosition;

    void Start()
    {
        boxCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        boxCollider.enabled = false;
        animationTriggered = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = initialPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            float deltaY = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;

            if (!animationTriggered)
            {
                if (deltaY > 0) // 위로 드래그
                {
                    animator.SetTrigger("TrOpen");
                    animationTriggered = true;
                }
                else if (deltaY < 0) // 아래로 드래그
                {
                    animator.SetTrigger("TrClose");
                    animationTriggered = true;
                }
            }

            isDragging = false;
            boxCollider.enabled = true;
        }
    }
}
