using UnityEngine;

public class Math : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    private void OnDrawGizmos()
    {
            Vector2 a = A.transform.position;
            Vector2 b = B.transform.position;


            Gizmos.color = Color.red;
            Gizmos.DrawLine(default, a);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(default, b);

            Gizmos.color = Color.red;

            // [ MAGNITUDE ]
            float aLength = Mathf.Sqrt(a.x * a.x + a.y * a.y); // same as a.magnitude

            // [ NORMALIZED ]
            Vector2 aNormalized = a / aLength; // same as a.normalized
            Gizmos.DrawSphere(aNormalized, 0.125f);

            // [ SCALAR PROJECTION ]
            Gizmos.color = Color.green;
            float scalarProjection = Vector2.Dot(a.normalized, b); // returns the angle of a and b. Good for checking if something is behind (negative) or in front (positive)

            // [ VECTOR PROJECTION ]
            Vector2 vectorProjection = a.normalized * scalarProjection; // projects a vectorpoint on the line of another vectorline
            Gizmos.DrawSphere(vectorProjection, 0.125f);
    }
}
