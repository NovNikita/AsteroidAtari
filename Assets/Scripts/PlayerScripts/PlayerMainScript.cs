using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float acceleration;

    PlayerWeaponBaseClass weapon;
    ControlsBaseClass controls;

    private Collider2D myCollider;
    private Animator myAnimator;

    public delegate void _PlayerKilledEvent();
    public event _PlayerKilledEvent PlayerKilledEvent;

    void Start()
    {
        weapon = GetComponent<PlayerWeaponBaseClass>();
        controls = GetComponent<ControlsBaseClass>();

        controls.SetParameters(GetComponent<Rigidbody2D>(), weapon, maxSpeed, turnSpeed, acceleration);

        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDestroyableByImpact destroyableByImpact = collision.GetComponent<IDestroyableByImpact>();
        if (destroyableByImpact != null)
        {
            destroyableByImpact.DestoyedByImpact();
            PlayerKilled();
        }
    }

    public void HitByEnemy()
    {
        PlayerKilled();
    }

    private void PlayerKilled()
    {
        if (gameObject.activeSelf)
        {
            GeneralSoundsScript.PlayExplosionSoundEffect();
            PlayerKilledEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }


    //Method to make player un-destroyable on timeToBeInvincible seconds
    public void SetInvincible(float timeToBeInvincible)
    {
        myCollider.enabled = false;
        myAnimator.SetBool("Invincible", true);
        Invoke("SetNonInvincible", timeToBeInvincible);
    }


    //Make player destroyable again
    private void SetNonInvincible()
    {
        myCollider.enabled = true;
        myAnimator.SetBool("Invincible", false);
    }


    //Switch control type by removing current controls script and adding new one
    public void ChangeControls(ControlsBaseClass.ControlType controlType)
    {
        Destroy(controls);

        switch (controlType)
        {

            case ControlsBaseClass.ControlType.Keyboard:
                controls = gameObject.AddComponent<PlayerKeyboardControls>();
                break;

            case ControlsBaseClass.ControlType.MouseKeyboard:
                controls = gameObject.AddComponent<PlayerMouseKeyboardControls>();
                break;

            default:
                controls = gameObject.AddComponent<PlayerKeyboardControls>();
                break;

        }

        controls.SetParameters(GetComponent<Rigidbody2D>(), weapon, maxSpeed, turnSpeed, acceleration);
    }

}
