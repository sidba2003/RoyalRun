using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float playerZClampValue;
    [SerializeField] float playerXClampValue;

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
        Vector3 movementPosition = player_rigidBody.position + movementVector * (Time.fixedDeltaTime * movementSpeed);

        movementPosition.x = Mathf.Clamp(movementPosition.x, -playerXClampValue, playerXClampValue);
        movementPosition.z = Mathf.Clamp(movementPosition.z, -playerZClampValue, playerZClampValue);

        player_rigidBody.MovePosition(movementPosition);
    }
}
