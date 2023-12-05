using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmozPivotPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, .05f);
        transform.localRotation = Quaternion.identity;
    }
}
