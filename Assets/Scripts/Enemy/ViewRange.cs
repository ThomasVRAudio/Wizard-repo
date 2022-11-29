using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ITargettable>() != null)
        {
            ITargettable target = other.GetComponent<ITargettable>();
            GetComponentInParent<EnemyStateManager>().SetTarget(target);
            GetComponentInParent<EnemyStateManager>().SwitchState(GetComponentInParent<EnemyChaseState>());
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<ITargettable>() != null)
        {
            GetComponentInParent<EnemyStateManager>().ResetTarget();
        }
    }
}
