using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerOfMass : MonoBehaviour
{


    public Vector3 newGravityCenter;
    public bool Awake;

    private Rigidbody carRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        //newGravityCenter = transform.position;
        carRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        carRigidBody.centerOfMass = newGravityCenter;
        carRigidBody.WakeUp();
        Awake = !carRigidBody.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position + transform.rotation * newGravityCenter, .1f);
    }
}
