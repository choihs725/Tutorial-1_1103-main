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
    public AudioClip accelerationSound; // ���� �Ҹ�
    public AudioClip brakeSound; // �극��ũ �Ҹ�

    //private XRController leftController;
    //private XRController rightController;
    private Brake_R brakeScript1;  // Brake_R ��ũ��Ʈ�� �����ϱ� ���� ����
    private brake brakeScript2;

    private AudioSource audioSource; // �Ҹ� �ҽ�

    public float forwardSpeed;
    public float turnSpeed;
    public float acceleration;
    public float brakeForce; // �극��ũ ��
    public float sidebrakeForce; // ���̵�극��ũ ��

    private float currentSpeed = 0.0f;
    private float maxVolume = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // XR Toolkit�� ���� ���ʰ� ������ ��Ʈ�ѷ� ��������
        //InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, new List<InputDevice>());
        //InputDevices.GetDevicesAtXRNode(XRNode.RightHand, new List<InputDevice>());
        //leftController = FindObjectOfType<XRController>();
        //rightController = FindObjectOfType<XRController>();
        // Brake_R ��ũ��Ʈ�� ã�Ƽ� �Ҵ�
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

        // ���ο� ȸ���� ����
        transform.Rotate(Vector3.up, turnAngle);

        // Check if both A and X buttons are pressed
        /*if (CheckBrakeInput())
        {
            // Apply brake force
            currentSpeed -= brakeForce * Time.deltaTime;
            // �극��ũ �Ҹ� ���
            audioSource.clip = brakeSound;
            audioSource.Play();
        }*/
        // �ӵ��� 0 �����̸� ���ߵ��� ����
        if (currentSpeed < 0.0f)
        {
            currentSpeed = 0.0f;
        }

        // ���ӵ� ����
        currentSpeed += acceleration * Time.deltaTime * (lever.value ? 0 : 1);
        // ���̵�극��ũ ����
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
        


        // ���� �̵�
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        //Debug.Log("Moving Forward");   
        
        // �ڵ� ���� ������ŭ ������ ȸ����Ŵ
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
