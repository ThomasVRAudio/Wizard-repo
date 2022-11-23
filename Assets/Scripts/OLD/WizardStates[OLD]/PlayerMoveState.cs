using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public float speed = 1f;
    PlayerStateManager player;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private float gravity = 20.0f;

    public override void EnterState(PlayerStateManager player) => this.player = player; 

    public override void UpdateState()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool move = (vertical > 0.5);
        player.animator.SetBool("run", move);

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

        if (direction.magnitude >= 0.1f)
        {         
            moveDir.y -= gravity * Time.deltaTime;
            player.CharacterController.Move(speed * Time.deltaTime * moveDir);
        }

        player.CharacterController.Move(new Vector3(0f, moveDir.y, 0f));

    }
}

//public float RotationSpeed = 240.0f;
//private float gravity = 20.0f;
//private Vector3 moveDir = Vector3.zero;
//PlayerStateManager player;


//float h = Input.GetAxis("Horizontal");
//float v = Input.GetAxis("Vertical");

//if (v < 0) v = 0;

//transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);

//if (player.CharacterController.isGrounded)
//{
//    bool move = (v > 0.5);

//    player.animator.SetBool("run", move);
//    moveDir = Vector3.forward * v;
//    moveDir = transform.TransformDirection(moveDir);
//    moveDir *= player.Speed;
//}

//moveDir.y -= gravity * Time.deltaTime;
//player.CharacterController.Move(moveDir * Time.deltaTime);
