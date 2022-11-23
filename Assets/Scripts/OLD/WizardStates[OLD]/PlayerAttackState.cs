using System.ComponentModel;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public ISpell spell;
    PlayerStateManager player;

    public override void EnterState(PlayerStateManager player)
    {
        this.player = player;

        if (player.SpellType == PlayerStateManager.Spells.Fire) GetSpell<FireSpell>();
        if (player.SpellType == PlayerStateManager.Spells.Earth) GetSpell<EarthSpell>(); 
        if (player.SpellType == PlayerStateManager.Spells.Tornado) GetSpell<TornadoSpell>();
        if (player.SpellType == PlayerStateManager.Spells.Air) GetSpell<AirSpell>();

        if (spell == null)
        {
            player.SwitchState(player.MoveState);
            return;
        }
        
        player.Speed = 0;
        player.HasCast = true;

        player.animator.SetBool("IsAttacking", true);
        spell.Cast(player.spellSpawnPos, player.animator);
        player.OnEndAnimationCallback += OnEndSpell;
    }

    void GetSpell<T>()
    {
        T getSpell = GetComponent<T>();
        spell = (getSpell == null) ? null : (ISpell)getSpell;

    }

    public override void UpdateState() { }

    void OnEndSpell()
    {
        player.animator.SetBool("IsAttacking", false);
        player.HasCast = false;
        player.SwitchState(player.MoveState);
        player.OnEndAnimationCallback -= OnEndSpell;
    }
}
