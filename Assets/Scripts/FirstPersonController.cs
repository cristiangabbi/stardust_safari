using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    //camera and player movement
    public float mouseSensitivity = 100f;
    public float movementSpeed = 10f;
    public float jumpMultiplier = 10f;
    public Camera cameraFP;
    public Transform character;

    //collision with terrain
    public Transform groundController;
    public float controlRadius = 0.4f;
    public LayerMask groundLayer;

    //interact with van
    public float interactionDistance = 10f;
    public LayerMask spaceshipLayer;
    RaycastHit interactionHit;

    //slope handling
    Rigidbody rb;
    float playerHeight;
    RaycastHit slopeHit;

    //camera movement through mouse
    float rotationX = 0f;
    float rotationY = 0f;

    //audioManager
    AudioManager audioManager;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //playerHeight = GetComponent<CapsuleCollider>().height;
        playerHeight = character.parent.GetComponent<CapsuleCollider>().bounds.size.y;

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        CameraMovement();

        // jump only allowed if player is grounded
        Collider[] groundCollisions = Physics.OverlapSphere(groundController.position, controlRadius, groundLayer);

        if (groundCollisions.Length != 0)
        {
            Jump();

            if (!audioManager.IsPlaying("Footsteps"))
                audioManager.Play("Footsteps");
        }
        else 
        {
            audioManager.Stop("Footsteps");
        }

        SurfaceController.isAllowedToEnterSpaceship = InteractWithSpaceship();
    }


    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);

        rotationY += mouseX;

        //camera local rotation
        cameraFP.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //character local rotation
        character.localRotation = Quaternion.Euler(0, rotationY, 0);
        
    }

    void PlayerMovement()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = slopeVector() * vertical;
        rb.MovePosition(transform.position + movement * movementSpeed * Time.deltaTime);

        //footstep sound
        audioManager.SetSongVolume("Footsteps", Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f));
}

    void Jump()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumped");
            Vector3 jumpForce = transform.up * jumpMultiplier * Time.deltaTime;
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }


    private Vector3 slopeVector()
    {
        Physics.Raycast(transform.position, -transform.up, out slopeHit, playerHeight / 2 + 0.5f, groundLayer);

        if(slopeHit.normal != transform.up)
        {
            Debug.DrawLine(transform.position, Vector3.ProjectOnPlane(character.transform.forward, slopeHit.normal), Color.red);
            return Vector3.ProjectOnPlane(character.transform.forward.normalized, slopeHit.normal);
        }
        else
        {
            return character.transform.forward;
        }
    }


    private bool InteractWithSpaceship()
    {

        Ray ray = cameraFP.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(cameraFP.transform.position, ray.direction * interactionDistance, Color.red);

        if (Physics.Raycast(ray, out interactionHit, interactionDistance, spaceshipLayer))
        {
            if( spaceshipLayer.value == 1 << interactionHit.transform.gameObject.layer)
            {
                return true;                
            }
        }

        return false;
    }
}
