using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingAudioManager : MonoBehaviour
{
    public AudioSource[] wheels;

    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("hello");
            foreach(AudioSource brakeSFX in wheels)
            {
                if(!brakeSFX.isPlaying)
                brakeSFX.Play();
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            foreach (AudioSource brakeSFX in wheels)
            {
                if(brakeSFX.isPlaying)
                brakeSFX.Stop();
            }
        }


        if(carController.frontDriverW.rpm == 0)
            foreach (AudioSource brakeSFX in wheels)
            {
                if (brakeSFX.isPlaying)
                    brakeSFX.Stop();
            }
    }
}
