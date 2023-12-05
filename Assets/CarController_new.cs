using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class CarController_new : MonoBehaviour
{
    public XRLever lever;
    public XRLever lever2;
    public XRKnob knob;
    public AudioClip accelerationSound; // 가속 소리
    public AudioClip brakeSound; // 브레이크 소리

    //private XRController leftController;
    //private XRController rightController;
    private Brake_R brakeScript1;  // Brake_R 스크립트에 접근하기 위한 변수
    private brake brakeScript2;

    private AudioSource audioSource; // 소리 소스

    public float forwardSpeed;
    public float turnSpeed;
    public float acceleration;
    public float brakeForce; // 브레이크 힘
    public float sidebrakeForce; // 사이드브레이크 힘

    private float currentSpeed = 0.0f;
    private float maxVolume = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // XR Toolkit을 통해 왼쪽과 오른쪽 컨트롤러 가져오기
        //InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, new List<InputDevice>());
        //InputDevices.GetDevicesAtXRNode(XRNode.RightHand, new List<InputDevice>());
        //leftController = FindObjectOfType<XRController>();
        //rightController = FindObjectOfType<XRController>();
        // Brake_R 스크립트를 찾아서 할당
        brakeScript1 = FindObjectOfType<Brake_R>();
        brakeScript2 = FindObjectOfType<brake>();

        audioSource = GetComponent<AudioSource>();
        currentSpeed = forwardSpeed;
        Debug.Log("void Start finished");
    }

    // Update is called once per frame
    void Update()
    {
        float turnInput = knob.value;
        float turnAngle = Mathf.Lerp(-1.0f,1.0f, turnInput) * turnSpeed * Time.deltaTime;
        
        turnAngle = Mathf.Clamp(turnAngle, -90.0f, 90.0f);

        // 새로운 회전을 적용
        transform.Rotate(Vector3.up, turnAngle);

        // Check if both A and X buttons are pressed
        /*if (CheckBrakeInput())
        {
            // Apply brake force
            currentSpeed -= brakeForce * Time.deltaTime;
            // 브레이크 소리 재생
            audioSource.clip = brakeSound;
            audioSource.Play();
        }*/
        // 속도가 0 이하이면 멈추도록 설정
        if (currentSpeed < 0.0f)
        {
            currentSpeed = 0.0f;
        }

        // 가속도 적용
        currentSpeed += acceleration * Time.deltaTime * (lever.value ? 0 : 1);
        // 사이드브레이크 적용
        currentSpeed -= sidebrakeForce * (lever2.value ? 0 : 1) * Time.deltaTime;


        if (brakeScript1.val && brakeScript2.val)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            if (currentSpeed > 0 && (!audioSource.isPlaying || audioSource.clip != brakeSound))
            {
                audioSource.clip = brakeSound;
                audioSource.volume = 1.0f;
                audioSource.Play();
            }
        }

        
        if (currentSpeed > 0 && !lever.value && (!audioSource.isPlaying || audioSource.clip != accelerationSound))
        {
            float desiredVolume = Mathf.Lerp(0.0f, maxVolume, currentSpeed / 30.0f);
            audioSource.clip = accelerationSound;
            audioSource.volume = desiredVolume;
            audioSource.Play();
        }

        if (!lever2.value)
        {
            if (currentSpeed > 0 && (!audioSource.isPlaying || audioSource.clip != brakeSound))
            {
                audioSource.clip = brakeSound;
                audioSource.volume = 1.5f;
                audioSource.Play();
            }
        }
        


        // 전진 이동
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        //Debug.Log("Moving Forward");   
        
        // 핸들 돌린 각도만큼 차량을 회전시킴
        float rotationAmount = turnInput * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }
    /*bool CheckBrakeInput()
    {
        bool val = false;
        if (leftController && rightController)
        {
            leftController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftAButton);
            rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightAButton);
            if (leftAButton && rightAButton)
            {
                Debug.Log("X and A Button Pressed");
                val = true;
            }
        }
        else
        {
            val = false;
        }
        return val;
    }*/
}
