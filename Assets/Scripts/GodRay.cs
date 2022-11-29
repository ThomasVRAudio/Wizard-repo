using System.Collections;
using UnityEngine;

public class GodRay : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
    }
    public void OnStop()
    {
        ps.Stop();
    }

}
