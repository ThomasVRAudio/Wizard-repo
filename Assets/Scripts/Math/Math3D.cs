using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math3D : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public Vector3 offset = new Vector3(0,0,0);
    private float time = 0;
    public AnimationCurve curve;

    private void OnDrawGizmos()
    {
        Vector3 a = A.transform.position;
        Vector3 b = B.transform.position;



        //B.transform.position = A.transform.rotation * (Vector3.left + offset) + A.transform.position;

        if (Physics.Raycast(C.transform.position, C.transform.forward, out RaycastHit hit, 100f))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 0.1f);

        }

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(C.transform.position, C.transform.forward * 20);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(A.transform.position, hit.point);

        //Debug.Log(C.transform.rotation * C.transform.forward);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            time = 0;
           
        if (Input.GetKey(KeyCode.Space))
        {
            if(time < 1)
            {
                D.transform.position = Vector3.Lerp(A.transform.position, B.transform.position, time);
                D.transform.position += Vector3.Lerp(Vector3.zero, 
                                                        Vector3.up, 
                                                        curve.Evaluate(time));
                time += Time.deltaTime * 2f;
            }
        }
    }
}
