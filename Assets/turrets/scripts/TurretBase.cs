using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    public float rotationModifierSideways = 1;
    public float rotationModifierUpways = 1;
    public float spindleRotationSpeed = 1;

    public GameObject body; //object to rotate horizontally
    public GameObject swingArms; //object to rotate vertically
    public GameObject[] spindles; //rotate when shooting

    public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        //HandleYMovement(Input.GetAxis("Mouse X") * rotationModifierSideways);
        //HandleXMovement(Input.GetAxis("Mouse Y") * rotationModifierUpways);

        //if (Input.GetMouseButton(0))
        //{
        //    HandleFire();
        //}

        AutoTarget();
    }

    void HandleXMovement(float distance)
    {
        swingArms.transform.Rotate(new Vector3(distance, 0, 0));
    }

    void HandleYMovement(float distance)
    {
        body.transform.Rotate(new Vector3(0, 0, distance));
    }

    void HandleFire()
    {
        foreach(GameObject spindle in spindles)
        {
            spindle.transform.Rotate(Vector3.up, spindleRotationSpeed);
        }
    }

    void AutoTarget()
    {
        // Determine which direction to rotate towards
        Vector3 bodyTargetDirection = target.position - body.transform.position;
        Vector3 swingArmsTargetDirection = target.position - swingArms.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotationModifierSideways * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 bodyNewDirection = Vector3.RotateTowards(body.transform.forward, bodyTargetDirection, singleStep, 0.0f);
        Vector3 swingArmsNewDirection = Vector3.RotateTowards(swingArms.transform.forward, swingArmsTargetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        Quaternion bodyLookRotation = Quaternion.LookRotation(bodyNewDirection);
        Quaternion swingArmsLookRotation = Quaternion.LookRotation(swingArmsNewDirection);

        //body.transform.rotation = new Quaternion(bodyLookRotation.x, bodyLookRotation.y, bodyLookRotation.z, bodyLookRotation.w);
        //swingArms.transform.rotation = new Quaternion(swingArmsLookRotation.x, swingArmsLookRotation.y, swingArmsLookRotation.z, swingArmsLookRotation.w)
    }
}
