using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testingScript : MonoBehaviour
{

    public Text txt;


    float steering = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        if(steering <= 30 && steering >= -30)
        {
            steering += 0.02f*h;
        }

        if(h == 0 && steering != 0)
        {
            if (steering > 0) steering -= .03f ; else steering += .03f;
            if (steering < .1f && steering > 0) steering = 0;
            if (steering > -.1f && steering < 0) steering = 0;
        }
        print(steering);
        txt.text = steering + " Degree";
    }
}
