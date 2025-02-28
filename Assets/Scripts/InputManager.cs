using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnJump = new UnityEvent();
    public UnityEvent OnDash = new UnityEvent();

    private Vector2 lastInput = Vector2.zero;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(moveX, moveY).normalized;

        if (input != lastInput)
        {
            OnMove?.Invoke(input);
            lastInput = input;
        }
        if (input.magnitude == 0 && lastInput.magnitude > 0)
        {
            OnMove?.Invoke(Vector2.zero);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnDash?.Invoke();
        }
    }
}
