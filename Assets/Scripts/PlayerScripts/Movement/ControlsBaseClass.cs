using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ControlsBaseClass :MonoBehaviour
{
    protected float maxSpeed;
    protected float turnSpeed;
    protected float acceleration;
    protected Rigidbody2D myRb;
    protected PlayerWeaponBaseClass playerWeapon;

    protected AudioSource thrustAudioSource;
    protected bool playThrustSound = false;

    public enum ControlType
    {
        Keyboard,
        MouseKeyboard
    }

    private void Awake()
    {
        thrustAudioSource = transform.Find("ThrustSound").GetComponent<AudioSource>();
    }

    public void SetParameters(Rigidbody2D playerRb, PlayerWeaponBaseClass playerWeapon, float maxSpeed, float turnSpeed, float acceleration)
    {
        this.myRb = playerRb;
        this.playerWeapon = playerWeapon;
        this.maxSpeed = maxSpeed;
        this.turnSpeed = turnSpeed;
        this.acceleration = acceleration;
    }

    public abstract void HandleTurning();

    public abstract void HandleAcceleration();

    public abstract void HandleShooting();
}
