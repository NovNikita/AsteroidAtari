using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseKeyboardControls : ControlsBaseClass
{

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }


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
        Vector3 vectorToTarget = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * turnSpeed);

    }


    public override void HandleAcceleration()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Mouse1))
        {

            myRb.AddRelativeForce(Vector3.up * acceleration);
            playThrustSound = true;

        }
        else

        {
            playThrustSound = false;
        }


        //Limit player speed
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerWeapon.Shoot();
        }
    }
}
