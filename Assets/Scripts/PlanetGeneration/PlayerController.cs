using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform bottomCollider;
    public float movementSpeed, pitchSpeed, yawSpeed, rollSpeed, hoverSpeed;
    public float movementAcceleration, pitchAcceleration, yawAcceleration, rollAcceleration, hoverAcceleration;
    Rigidbody rb;
    float currentSpeed, currentPitch, currentYaw, currentRoll, currentHover;


    AudioManager audioManager;


    //landing
    public LayerMask planetLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = 0f;

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotation movement
        currentPitch = Mathf.Lerp(currentPitch, Input.GetAxis("Vertical") * pitchSpeed, pitchAcceleration * Time.deltaTime);
        currentYaw = Mathf.Lerp(currentYaw, Input.GetAxis("Horizontal") * yawSpeed, yawAcceleration * Time.deltaTime);
        currentRoll = Mathf.Lerp(currentRoll, Input.GetAxis("Roll") * rollSpeed, rollAcceleration * Time.deltaTime);

        transform.Rotate(currentPitch, currentYaw, currentRoll, Space.Self);

        //vertical hovering
        Hover();

        //forward movement
        currentSpeed = Mathf.Lerp(currentSpeed, Input.GetAxis("Accelerate") * movementSpeed, movementAcceleration * Time.deltaTime);
        Vector3 forwardForce = currentSpeed * transform.forward;

        rb.AddForce(forwardForce, ForceMode.Impulse);

        audioManager.SetSongVolume("SpaceshipSound", Mathf.Clamp(Input.GetAxis("Accelerate"), 0f, 0.2f));

        //check if player is allowed to exit spaceship
        SurfaceController.isAllowedToExitSpaceship = isLanded();
    }


    void Hover()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentHover = Mathf.Lerp(currentHover, hoverSpeed, hoverAcceleration * Time.deltaTime);
            Vector3 hoverForce = transform.up * currentHover;
            rb.AddForce(hoverForce, ForceMode.Impulse);
        }
        else
        {
            currentHover = 0f;
        }
    }



    //private void OnCollisionEnter(Collision collision)

    public bool isLanded()
    {
        //check collision with layer
        bool collision = Physics.CheckSphere(bottomCollider.position, 0.6f, LayerMask.GetMask("PlanetTerrain"));

        if(collision)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(bottomCollider.position, 0.6f);
    }
}
