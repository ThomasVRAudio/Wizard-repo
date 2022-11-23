public class PlayerHitState : PlayerBaseState
{
    PlayerStateManager player;
    public override void EnterState(PlayerStateManager player)
    {
        if (player.IsImmune) { return; } else { player.IsImmune = true; }
        this.player = player;

        player.Health -= player.Damage;
        player.Speed = 0;

        if (player.Health <= 0)
        {
            player.SwitchState(player.DeathState);
            return;
        }

        player.animator.SetBool("isRecovering", true);
        player.animator.SetTrigger("Hit");
        player.OnEndAnimationCallback += OnEndAnimation;
    }

    public override void UpdateState() { }

    private void OnEndAnimation()
    {
        player.animator.SetBool("isRecovering", false);
        player.SwitchState(player.MoveState);
        player.IsImmune = false;
        player.OnEndAnimationCallback -= OnEndAnimation;
    }
}
