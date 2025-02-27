using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    private Rigidbody player;

    private void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        player = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        player.AddForce(speed * moveDirection);
    }
}