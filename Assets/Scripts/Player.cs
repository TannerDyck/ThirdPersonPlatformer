using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    private Rigidbody player;
    private Transform cameraTransform;

    private void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        player = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        transform.forward = cameraForward.normalized;
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * direction.y + right * direction.x;
        player.AddForce(speed * moveDirection);
    }
}
