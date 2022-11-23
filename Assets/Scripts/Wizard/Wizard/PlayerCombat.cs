using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public ISpell spell;
    public Transform spellSpawnPos;
    private bool hasCast = false;

    Player player;

    public delegate void OnMouseUpEvent();
    public event OnMouseUpEvent MouseUpEvent;

    private void Awake() => player = GetComponent<Player>();

    public void CombatUpdate(Player player)
    {
        if (Input.GetMouseButtonUp(0)) MouseUpEvent?.Invoke();


        if (hasCast)
            return;

        if (Input.GetMouseButtonDown(0)) SpellCast(GetComponent<FireSpell>());
        if (Input.GetKeyDown(KeyCode.Q)) SpellCast(GetComponent<ShieldSpell>());
        if (Input.GetKeyDown(KeyCode.E)) SpellCast(GetComponent<EarthSpell>());
        if (Input.GetKeyDown(KeyCode.R)) SpellCast(GetComponent<TornadoSpell>());
        if (Input.GetKeyDown(KeyCode.T)) SpellCast(GetComponent<AirSpell>());
    }

    void SpellCast(ISpell SpellType)
    {
        if (SpellType == null) return;

        spell = SpellType;
        hasCast = true;
        player.speed = 0;

        spell.Cast(spellSpawnPos, player.animator);
        player.OnEndAnimationCallback += OnEndAnimation;
        player.animator.SetBool("IsAttacking", true);
    }

    private void OnEndAnimation()
    {
        hasCast = false;
        player.speed = player.startSpeed;
        player.animator.SetBool("IsAttacking", false);
        player.OnEndAnimationCallback -= OnEndAnimation;
    }
}
