using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasettePlayer : MonoBehaviour
{
    public Vector3 tapeInsertPosition;
    private Collider playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider>();
    }

    public bool IsTapeInside(Collider tapeCollider)
    {
        return playerCollider.bounds.Intersects(tapeCollider.bounds);
    }
}
