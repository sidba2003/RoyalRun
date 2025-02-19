using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    Vector2 movement;
    Rigidbody player_rigidBody;

    void Awake()
    {
        player_rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movementVector = new Vector3(movement.x, 0f, movement.y);
        player_rigidBody.MovePosition(player_rigidBody.position + movementVector * (Time.fixedDeltaTime * movementSpeed));
    }
}
