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

        if (Input.GetMouseButtonDown(0)) SpellCast(GetComponent<FireSpell>(), PlayerStats.Instance.FireTimer);
        if (Input.GetKeyDown(KeyCode.Q)) SpellCast(GetComponent<ShieldSpell>(), PlayerStats.Instance.ShieldTimer);
        if (Input.GetKeyDown(KeyCode.E)) SpellCast(GetComponent<EarthSpell>(), PlayerStats.Instance.EarthTimer);
        if (Input.GetKeyDown(KeyCode.R)) SpellCast(GetComponent<TornadoSpell>(), PlayerStats.Instance.TornadoTimer);
        if (Input.GetKeyDown(KeyCode.T)) SpellCast(GetComponent<AirSpell>(), PlayerStats.Instance.AirTimer);

    }

    void SpellCast(ISpell SpellType, float time)
    {
        if (SpellType == null) return;

        if (SpellTimer.Instance.GetTime(SpellType) > 0)
            return;

        spell = SpellType;
        hasCast = true;
        player.speed = 0;

        spell.Cast(spellSpawnPos, player.animator);
        SpellTimer.Instance.StartTimer(spell, time);
        SkillUI.Instance.SetInactiveColor(spell);
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
