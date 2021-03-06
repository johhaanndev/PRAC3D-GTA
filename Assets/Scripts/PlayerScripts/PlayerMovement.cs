using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private Rigidbody rb;
    private Camera cam;

    [Header("Movement")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    [Header("Jump")]
    public float jumpForce = 3f;
    public float gravityValue = -9.81f;
    private Vector3 gravity;

    public Camera mainCamera;

    [Header("Drive parameters")]
    public bool isDriving = false;
    public SkinnedMeshRenderer meshRenderers;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerHealth>().isDead)
        {
            if (!isDriving)
            {
                controller.enabled = true;
                BasicMovement();
            }
            else
            {
                controller.enabled = false;
            }
        }
    }

    private void BasicMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }


        anim.SetFloat("Speed", direction.magnitude);
        controller.Move(direction * speed * Time.deltaTime);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(pointToLook);
        }

        gravity.y += gravityValue * Time.deltaTime;
        controller.Move(gravity);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDriving = true;
                other.GetComponent<CarEnterExit>().enabled = true;
                other.GetComponent<CarMovement>().enabled = true;
            }
        }
    }
}
