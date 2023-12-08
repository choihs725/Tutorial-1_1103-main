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
        // Brake_R 스크립트를 찾아서 할당
        brakeScript1 = FindObjectOfType<Brake_R>();
        brakeScript2 = FindObjectOfType<brake>();

        audioSource = GetComponent<AudioSource>();
        currentSpeed = forwardSpeed;
        Debug.Log("void Start finished");
        audioSource.clip = accelerationSound;
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
        /*
        if (currentSpeed < 0.0f)
        {
            currentSpeed = 0.0f;
        }
        */

        if (currentSpeed > 0)
        {
            float desiredVolume = Mathf.Lerp(0.0f, maxVolume, currentSpeed / 10.0f);
            float desiredBrakeVolume = Mathf.Lerp(0.0f, 1.0f, currentSpeed / 10.0f);
            float desiredSideBrakeVolume = Mathf.Lerp(2.0f, 0.0f, currentSpeed / 10.0f);
            if (!lever.value) // 기어가 D에 있을 때
            {
                /*
                if (audioSource.clip == accelerationSound)
                {
                    float desiredVolume = Mathf.Lerp(0.0f, maxVolume, currentSpeed / 10.0f);
                    audioSource.clip = accelerationSound;
                    audioSource.volume = desiredVolume;
                    if (!audioSource.isPlaying)
                    {
                        Debug.Log("Sound"); 
                        audioSource.Play();
                    }
         
                }
                */
                if (brakeScript1.val && brakeScript2.val) //브레이크를 밟았을 때
                {
                    if (audioSource.clip != brakeSound) 
                    { 
                        audioSource.Stop();
                        
                        audioSource.clip = brakeSound;
                        
                        //audioSource.PlayOneShot(accelerationSound, desiredVolume);
                        //audioSource.PlayOneShot(brakeSound, desiredBrakeVolume);

                    }
                    
                    currentSpeed -= brakeForce * Time.deltaTime;
                    
                    

                    if (!audioSource.isPlaying)
                        {
                            audioSource.clip = brakeSound;
                            Debug.Log("Sound");
                            audioSource.PlayOneShot(accelerationSound, desiredVolume);
                            audioSource.PlayOneShot(brakeSound, desiredBrakeVolume);
                    }

                    
                    if (!lever2.value) // 사이드브레이크 작동
                    {
                        if (audioSource.clip != brakeSound)
                        {
                            audioSource.Stop();
                            audioSource.clip = brakeSound;
                            //audioSource.PlayOneShot(accelerationSound, desiredVolume);
                            //audioSource.PlayOneShot(brakeSound, desiredSideBrakeVolume);
                        }
                        if (!audioSource.isPlaying)
                        {
                            audioSource.PlayOneShot(accelerationSound, desiredVolume);
                            audioSource.PlayOneShot(brakeSound, desiredSideBrakeVolume);
                        }
                    }
                }
                else //브레이크를 밟지 않았을 때
                {
                    if(audioSource.clip != accelerationSound)
                    {
                        audioSource.Stop();

                    }
                    
                    audioSource.clip = accelerationSound;
                    audioSource.volume = desiredVolume;
                    if (!audioSource.isPlaying)
                    {
                        Debug.Log("Sound");
                        audioSource.Play();
                    }
                    if (!lever2.value) // 사이드 브레이크 작동
                    {
                        if (audioSource.clip != brakeSound)
                        {
                            audioSource.Stop();
                            audioSource.clip = brakeSound;
                        }
                        //audioSource.clip = brakeSound;
                        
                        audioSource.volume = desiredBrakeVolume;

                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = brakeSound;
                            audioSource.Play();
                        }
                    }
                }
            }
            else // 기어가 N일 때
            {
                if (audioSource.clip == accelerationSound)
                {
                    audioSource.Stop();
                }
                


                if (brakeScript1.val && brakeScript2.val) //브레이크를 밟았을 때
                {
                    if (audioSource.clip != brakeSound)
                    {
                        audioSource.Stop();
                        audioSource.clip = brakeSound;

                    }

                    currentSpeed -= brakeForce * Time.deltaTime;

                    if (!audioSource.isPlaying)
                    {
                        Debug.Log("Sound");
                        audioSource.clip = brakeSound;
                        audioSource.volume = desiredBrakeVolume;
                        audioSource.Play();
                    }
                }
                else //브레이크를 밟지 않았을 때
                {
                    if (audioSource.clip == brakeSound)
                    {
                        audioSource.Stop();
                    }

                    if (!lever2.value) // 사이드 브레이크 작동
                    {
                        if (audioSource.clip != brakeSound)
                        {
                            audioSource.Stop();
                            audioSource.clip = brakeSound;
                        }
                        //audioSource.clip = brakeSound;

                        audioSource.volume = desiredSideBrakeVolume;

                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = brakeSound;
                            audioSource.Play();
                        }
                    }
                }
            }
        }
        /*
        if(brakeScript1.val && brakeScript2.val) //브레이크를 밟았을 때
        {
            currentSpeed -= brakeForce * Time.deltaTime;
            if (!audioSource.isPlaying || audioSource.clip != brakeSound)
            {
                audioSource.clip = brakeSound;
                audioSource.volume = 1.0f;
                audioSource.Play();
            }
        }
        else
        {
            if((audioSource.clip == brakeSound) && lever2.value)
            {
                audioSource.Stop();
            }
        }

        if (!lever2.value) // 사이드브레이크 작동
        {
            if(!audioSource.isPlaying || audioSource.clip != brakeSound){
                audioSource.clip = brakeSound;
                audioSource.volume = 1.5f;
                audioSource.Play();
            }
        }
        else
        {
            if((audioSource.clip == brakeSound) && !(brakeScript1.val && brakeScript2.val)) {
                audioSource.Stop();
            }
        }
    }*/
        else
        {
            currentSpeed = 0.0f;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // 가속도 적용
        currentSpeed += acceleration * Time.deltaTime * (lever.value ? 0 : 1);
        // 사이드브레이크 적용
        currentSpeed -= sidebrakeForce * (lever2.value ? 0 : 1) * Time.deltaTime;

        // 전진 이동
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        //Debug.Log("Moving Forward");   
        
        // 핸들 돌린 각도만큼 차량을 회전시킴
        float rotationAmount = turnInput * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);
    }
}
