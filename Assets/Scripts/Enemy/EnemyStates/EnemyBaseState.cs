using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState();

}

