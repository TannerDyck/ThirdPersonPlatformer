using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();

    void Update()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector2.up;
            Debug.Log($"Player is pressing W");
        }
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
            Debug.Log($"Player is pressing A");
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector2.down;
            Debug.Log($"Player is pressing S");
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
            Debug.Log($"Player is pressing D");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Player is pressing SPACE");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log($"Player is pressing LeftControl");
        }
        OnMove?.Invoke(input);
    }
}