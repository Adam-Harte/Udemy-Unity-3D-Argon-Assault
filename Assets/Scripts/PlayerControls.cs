using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("User Input Actions")]
    [SerializeField]
    InputAction movement;
    [SerializeField]
    InputAction fire;

    [Header("General Setup Settings")]
    [Tooltip("Controls how fast the player moves based on input")]
    [SerializeField]
    float controlSpeed = 10f;
    [Tooltip("Max range player can move left and right")]
    [SerializeField]
    float xRange = 10f;
    [Tooltip("Max range player can move up and down")]
    [SerializeField]
    float yRange = 7f;
    [SerializeField]
    GameObject[] lasers;

[Header("Rotation Factors")]
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
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
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

    private void ProcessFiring()
    {
        if (fire.IsPressed())
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
