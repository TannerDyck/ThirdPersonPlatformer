/*
A lot of this code was inspired by Rytech's YouTube video "SMOOTH FIRST PERSON MOVEMENT in Unity"
Other parts were inspired by previous labs guides, previous revisions of this code, and various other online videos/threads.
*/
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
