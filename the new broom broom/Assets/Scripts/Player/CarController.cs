using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    private BoxCollider controller;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    public GameObject explosion;

    private float lastPosition;

    [SerializeField] public float motorForce;
    [SerializeField] public float breakForce;
    [SerializeField] public float maxMotorForce;
    [SerializeField] public float maxBreakForce;
    [SerializeField] public float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Rigidbody rb;
    private float vel;

    public TextMeshProUGUI warningText;
    public int minSpeed;

    private void Start()
    {
        controller = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        warningText.enabled = false;
    }

    private void FixedUpdate()
    {
        if ((motorForce < maxMotorForce || breakForce < maxBreakForce) && PlayerManager.isGameStarted)
        {
            motorForce += 0.05f;
            breakForce += 0.085f;
        }

        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        vel = rb.velocity.magnitude;
        if ((gameObject.transform.position.y < -5 && PlayerManager.isGameStarted) || 
            (vel <= minSpeed && PlayerManager.isGameStarted && gameObject.transform.position.z > -60 )) {
            OnDeath();
        }

        if (vel < minSpeed+3 && vel > minSpeed && gameObject.transform.position.z > -72) {
            warningText.enabled = true;
        } else
        {
            warningText.enabled = false;
        }
    }

    private void GetInput()
    {
        if (PlayerManager.isGameStarted)
        {
            horizontalInput = Input.GetAxis(HORIZONTAL);
            verticalInput = Input.GetAxis(VERTICAL);
            isBreaking = Input.GetKey(KeyCode.DownArrow);
        }
        else return;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.SetPositionAndRotation(pos, rot);
    }
    private void OnDeath()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(expl, 3);
        PlayerManager.gameOver = true;
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Terrain")
        {
            OnDeath();
        }
    }
}