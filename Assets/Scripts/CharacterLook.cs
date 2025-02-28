using UnityEngine;
using Unity.Cinemachine;

public class CharacterLook : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        CinemachineVirtualCameraBase cinemachineCamera = FindFirstObjectByType<CinemachineVirtualCameraBase>();

        if (cinemachineCamera != null)
        {
            cameraTransform = cinemachineCamera.transform;
        }
        else
        {
            Debug.LogError("CharacterLook: Cinemachine Camera not found!");
        }
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;

        if (cameraForward.magnitude > 0.1f)
        {
            transform.forward = cameraForward;
        }
    }
}
