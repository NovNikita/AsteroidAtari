using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardControls : ControlsBaseClass
{

    //Turning and acceleration are in separate updated due to acceleration being physics related
    private void Update()
    {
        HandleTurning();
        HandleShooting();

    }

    private void FixedUpdate()
    {
        HandleAcceleration();
    }


    public override void HandleTurning()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * turnSpeed * Time.deltaTime);
        }
    }


    public override void HandleAcceleration()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            myRb.AddRelativeForce(Vector3.up * acceleration);
            playThrustSound = true;

        }
        else

        {
            playThrustSound = false;
        }


        //limit player speed
        if (myRb.velocity.magnitude > maxSpeed) 
            myRb.velocity = myRb.velocity.normalized * maxSpeed;

        ThrustSoundEffectCheck();
    }


    private void ThrustSoundEffectCheck()
    {
        if (!thrustAudioSource.isPlaying && playThrustSound)
            thrustAudioSource.Play();

        if (thrustAudioSource.isPlaying && !playThrustSound)
            thrustAudioSource.Stop();
    }


    public override void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerWeapon.Shoot();
        }
    }
}
