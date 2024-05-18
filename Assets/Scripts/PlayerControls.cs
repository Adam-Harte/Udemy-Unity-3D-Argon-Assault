using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    InputAction movement;
    [SerializeField]
    float controlSpeed = 10f;
    [SerializeField]
    float xRange = 10f;
    [SerializeField]
    float yRange = 7f;
    [SerializeField]
    float positionPitchFactor = -2f;
    [SerializeField]
    float controlPitchFactor = -15f;
    [SerializeField]
    float positionYawFactor = 2f;
    [SerializeField]
    float controlRollFactor = -15f;

    float horizontalThrow;
    float verticalThrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = horizontalThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        horizontalThrow = movement.ReadValue<Vector2>().x;
        verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow * controlSpeed * Time.deltaTime;
        float yOffset = verticalThrow * controlSpeed * Time.deltaTime;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
