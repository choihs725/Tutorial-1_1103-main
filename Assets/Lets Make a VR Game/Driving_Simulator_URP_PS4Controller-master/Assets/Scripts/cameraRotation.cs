using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{

    public float rotationSpeed;
    public float minY, MaxY;
    public float minX, MaxX;

    private Quaternion localRotation;
    // Start is called before the first frame update
    void Start()
    {
        localRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        localRotation.y += Input.GetAxis("CameraRotationY") * Time.deltaTime * rotationSpeed;
        localRotation.x += Input.GetAxis("CameraRotationX") * Time.deltaTime * rotationSpeed;

        localRotation.y = Mathf.Clamp(localRotation.y, minY, MaxY);
        localRotation.x = Mathf.Clamp(localRotation.x, minX, MaxX);


        transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, localRotation.z);
    }
}
