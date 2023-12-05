using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GearSwitch : MonoBehaviour
{
    public Text[] gearBoxHints;
    float gearBoxValueHolder = 0;
    private void Update()
    {



        if (gearBoxValueHolder != CarController.GearBox)
        {
            gearBoxValueHolder = CarController.GearBox;
            if (gearBoxValueHolder != 0)
            {
                if (gearBoxValueHolder == -1)
                {
                    resetColor();
                    gearBoxHints[5].color = Color.green;
                }
                else
                {
                    resetColor();
                    colorSpecificGear();
                }
            }
            else { 
                resetColor(); 
                }
        }
    }


    void resetColor()
    {
        for(int i = 0; i < gearBoxHints.Length; i++)
        {
            gearBoxHints[i].color = Color.white;
        }
    }

    void colorSpecificGear()
    {
        for(int i = 0; i < gearBoxHints.Length; i++)
        {
            if (gearBoxHints[i].name.Equals("" + CarController.GearBox))
                gearBoxHints[i].color = Color.green;
        }
    }
}
