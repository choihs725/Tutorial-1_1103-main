
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CarController : MonoBehaviour
{
    private void changeDirection()
    {
        if (Input.GetKeyUp(KeyCode.JoystickButton5) && GearBox <5) GearBox++;
        if (Input.GetKeyUp(KeyCode.JoystickButton4) && GearBox > -1) GearBox--;
    }
    
    public void getInput()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");
        if (GearBox >= 0)
        {
            
                m_VerticalInput = Input.GetAxis("Vertical") * 0.5f + 0.5f;
            //print(m_VerticalInput);
        }

        /*if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            m_VerticalInput = 0;
        }
        */
        if(GearBox == -1)
        {
            m_VerticalInput = (Input.GetAxis("Vertical") * 0.5f + 0.5f) * -1;
        }

    }

    public void steer()
    {
        // Temporary bloc of code
        if(currentSpeed > 60)
        {
            maxAngel = 5;
            minAngel = -5;
        }
        else
        {
            maxAngel = 30;
            minAngel = -30;
        }
        // Temporary bloc of code


        if (steerAngle_Calculations <= maxAngel && steerAngle_Calculations >= minAngel)
        {
            steerAngle_Calculations += 0.2f * m_HorizontalInput * Time.deltaTime * steeringSpeed;
        }

        if (m_HorizontalInput == 0 && steerAngle_Calculations != 0)
        {
            if (steerAngle_Calculations > 0) steerAngle_Calculations -= .15f * Time.deltaTime * steeringSpeed;
            else steerAngle_Calculations += .15f * Time.deltaTime * steeringSpeed;

            if (steerAngle_Calculations < .5f && steerAngle_Calculations > 0) steerAngle_Calculations = 0;
            if (steerAngle_Calculations > -.5f && steerAngle_Calculations < 0) steerAngle_Calculations = 0;
        }


        Vector3 rotation = new Vector3(25, steeringWheel.rotation.y, steerAngle_Calculations * 18 *-1);

        steeringWheel.localRotation = Quaternion.Euler(rotation);

        m_SteeringAngle = steerAngle_Calculations;
        frontPassengerW.steerAngle = m_SteeringAngle;
        frontDriverW.steerAngle = m_SteeringAngle;
    }

    private void accelerate()
    {
        currentSpeed = (float)(rearPassengerW.radius * rearPassengerW.rpm * 2 * 3.14 * 60 / 1000);

        currentSpeed = Mathf.RoundToInt(currentSpeed);

        /*Check this one*/ if (m_VerticalInput == -1) m_VerticalInput /= 6;

        if (GearBox != -1)
        {
            backWardLight.SetActive(false);
            if (currentSpeed < maxSpeedPerGear[GearBox])
            {
                motorForce = motorForcePerGearBox[GearBox];
                RearDriverW.motorTorque = m_VerticalInput * motorForce;
                rearPassengerW.motorTorque = m_VerticalInput * motorForce;
            }
            else
            {
                RearDriverW.motorTorque = 0;
                rearPassengerW.motorTorque = 0;
            }
        }
        else
        {
            backWardLight.SetActive(true);
            if (currentSpeed < 30)
            {
                motorForce = 1200;
                RearDriverW.motorTorque = m_VerticalInput * motorForce;
                rearPassengerW.motorTorque = m_VerticalInput * motorForce;
            }
            else
            {
                RearDriverW.motorTorque = 0;
                rearPassengerW.motorTorque = 0;
            }
        }
    }
    public void brake()
    {
        //handBrake
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handBrake = true;
            frontDriverW.brakeTorque = handBrakeForce;
            frontPassengerW.brakeTorque = handBrakeForce;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            handBrake = false;
            frontDriverW.brakeTorque = 0;
            frontPassengerW.brakeTorque = 0;
        }

        //Brake
        if (Input.GetAxis("BrakeAxis") > 0)
        {
            brakeLight.SetActive(true);
            RearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
        }

        if (Input.GetAxis("BrakeAxis")<0)
        {
            brakeLight.SetActive(false);
            RearDriverW.brakeTorque = 0;
            rearPassengerW.brakeTorque = 0;
        }


    }

   

    
    
    private void carLight()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            headLightLeft.SetActive(!headLightLeft.activeSelf);
            headLightRight.SetActive(!headLightRight.activeSelf);
        }

        float indicators = Input.GetAxis("Indicators");

        if (indicators != 0)
        {

            if (indicators == 1) rightIndicators.SetActive(rightIndicators.activeSelf);
            if (indicators == -1) LeftIndicators.SetActive(LeftIndicators.activeSelf);

        }
    }
    private void updateWheelsPoses()
    {
        updateWheelPose(frontDriverW, frontDriverT);

        updateWheelPose(frontPassengerW, frontPassengerT);

        updateWheelPose(RearDriverW, rearDriverT);

        updateWheelPose(rearPassengerW, RearPassengerT);
    }

    private void updateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
        
    }

    void engineTurnPerSecond()
    {
        
    }

    void startEngineToggle()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            startEngine = true;
        }
    }
    private void Awake()
    {
        maxAngel = 30;
        minAngel = -30;
    }
    private void Update()
    {
        startEngineToggle();
        if (startEngine)
        {
            changeDirection();
            getInput();
            steer();
            accelerate();
            brake();
            updateWheelsPoses();
            carLight();
        }
        // print(frontPassengerW.rpm);
        // detectPressedKey();

        if (Input.GetKeyUp(KeyCode.T))
        {
            print(rearPassengerW.rpm);
        }

    }

    void detectPressedKey()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(kcode))
            {
                Debug.Log("keycode is: " + kcode);
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void FixedUpdate()
    {
        speedText.text = Mathf.Abs((int)currentSpeed)+" km/h";

        speedSlider.value = Mathf.Abs(currentSpeed * 1.5f);

        indicatorRotation = new Vector3(-165,0,-25+speedSlider.value);
        speedIndicator.localRotation = Quaternion.Euler(indicatorRotation);

        if (GearBox < 0)
        {
            float powerPercentage = (currentSpeed * 100) / 30;
            powerPercentage /= 100;
            rpmSlider.value = m_VerticalInput * 400 + 100 * powerPercentage;
        }

        if (GearBox > 0)
        {
            float powerPercentage = (currentSpeed * 100) / maxSpeedPerGear[GearBox];
            powerPercentage /= 100;
            rpmSlider.value = (m_VerticalInput * 400 * powerPercentage) + 100;
        }
        else
        {
            rpmSlider.value = m_VerticalInput * 400 + 100;
        }
    }

    private float m_HorizontalInput;
    private float m_VerticalInput;
    private float m_SteeringAngle;
    public static int GearBox = 0;

    public bool startEngine;

    public WheelCollider frontPassengerW, frontDriverW;
    public WheelCollider rearPassengerW, RearDriverW;
    public Transform frontPassengerT, frontDriverT;
    public Transform RearPassengerT, rearDriverT;

    [Range(-30,30)]
    public float steerAngle_Calculations;
    public float maxAngel, minAngel;
    public float steeringSpeed;
    public Transform steeringWheel;
    
    public float motorForce = 50;
    public float[] motorForcePerGearBox;
    public bool handBrake = false;
    public float handBrakeForce;
    public float brakeForce;
    public float currentSpeed;
    public float[] maxSpeedPerGear;

    public GameObject headLightRight, headLightLeft;
    public GameObject brakeLight;
    public GameObject backWardLight;
    public GameObject rightIndicators, LeftIndicators;
    public Text speedText;

    public Vector3 indicatorRotation;
    public Slider speedSlider;
    public Slider rpmSlider;
    public Transform speedIndicator;
    public Transform rpmIndicator;
}
