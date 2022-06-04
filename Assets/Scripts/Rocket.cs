using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TODO
 * - turn on gravity and try to manage it (or set gravity to false in here)
 * - 
 */
[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float maxAngularVelocity = 20;

    [SerializeField]
    private float thrust = 20;

    [SerializeField]
    [Range(-10, 10)]
    private float rotKp, rotKi, rotKd;

    private PID_Controller xAxisController;
    private PID_Controller yAxisController;
    private PID_Controller zAxisController;
    
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void FixedUpdate() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // TODO MOVE THIS TO START ONCE PID IS TUNED
        rigidbody.maxAngularVelocity = maxAngularVelocity;
        xAxisController = new PID_Controller(rotKp, rotKi, rotKd);
        yAxisController = new PID_Controller(rotKp, rotKi, rotKd);
        zAxisController = new PID_Controller(rotKp, rotKi, rotKd);
        //

        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 rotationDirection = Vector3.RotateTowards(-transform.forward, targetDirection, 360, 0.00f);
        Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);

        float xAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.x);
        float xTorqueCorrection = xAxisController.GetOutput(xAngleError, Time.fixedDeltaTime);
        float yAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation.eulerAngles.y);
        float yTorqueCorrection = yAxisController.GetOutput(yAngleError, Time.fixedDeltaTime);
        float zAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, targetRotation.eulerAngles.z);
        float zTorqueCorrection = zAxisController.GetOutput(zAngleError, Time.fixedDeltaTime);

        Vector3 torque = xTorqueCorrection * Vector3.right + yTorqueCorrection * Vector3.up + zTorqueCorrection * Vector3.forward;

        rigidbody.AddRelativeTorque(torque);

        transform.position = transform.position + transform.forward * thrust;
        //float totalErrorAngle = Vector3.Angle(targetDirection, transform.forward);
        // 1 if in front, -1 if behind rocket
        //float thrustScale = (90 - totalErrorAngle) / 90;
        //rigidbody.AddRelativeForce(thrustScale * thrust * -Vector3.forward * Time.fixedDeltaTime);
    }

}
