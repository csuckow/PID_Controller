using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PID_Controller
{
    public float Kp { get; set; }
    public float Ki { get; set; }
    public float Kd { get; set; }

    private float p, i, d;
    private float previousError;

    public PID_Controller(float kp, float ki, float kd) {
        this.Kp = kp;
        this.Ki = ki;
        this.Kd = kd;
    }

    public float GetOutput(float currentError, float deltaTime) {
        this.p = currentError;

        this.i += p * deltaTime;
        
        this.d = (currentError - previousError) / deltaTime;
        
        this.previousError = currentError;
        
        //Debug.Log("P=" + this.p * Kp + ", I=" + this.i * Ki + ", D=" + this.d * Kd + "; TOTAL OUTPUT=" + (this.p * Kp + this.i * Ki + this.d * Kd));
        return this.p*Kp + this.i*Ki + this.d*Kd;
    }
}