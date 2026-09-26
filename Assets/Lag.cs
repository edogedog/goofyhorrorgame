using UnityEngine;

public class FlashlightLag : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float followSpeed = 8f;

    private void LateUpdate()
    {
        transform.position = cameraTransform.position;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            cameraTransform.rotation,
            followSpeed * Time.deltaTime
        );
    }
}