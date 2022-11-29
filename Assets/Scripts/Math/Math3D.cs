using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math3D : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    //public GameObject C;
    //public GameObject D;
    //public Vector3 offset = new Vector3(0,0,0);
   // private float time = 0;
   // public AnimationCurve curve;

    private void OnDrawGizmos()
    {
        Vector3 a = A.transform.position;
        Vector3 b = B.transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(a, a + (b - a).normalized);
        //Gizmos.DrawRay(a, a + (b - a).normalized);

        Gizmos.color = Color.blue;

        Gizmos.color = Color.cyan;

    }
}
