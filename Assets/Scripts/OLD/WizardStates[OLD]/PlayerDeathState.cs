public class PlayerDeathState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player) => player.animator.SetTrigger("Die");
    public override void UpdateState() { }
}
