using System;
using UnityEngine;

public interface ISpell
{
    public void Cast(Transform spawnPos, Animator animator);
}
