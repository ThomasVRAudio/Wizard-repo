using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private float gravity = 20.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void MovementUpdate(Player player)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;



        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            

        if (direction.magnitude >= 0.1f)
        {
            player.animator.SetBool("run", true);
            moveDir.y -= gravity * Time.deltaTime;
            characterController.Move(player.speed * Time.deltaTime * moveDir);
        } else { player.animator.SetBool("run", false); }

        characterController.Move(new Vector3(0f, moveDir.y, 0f));
    }
}
