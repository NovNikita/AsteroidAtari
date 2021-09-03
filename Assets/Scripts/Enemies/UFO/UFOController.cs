using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOController : MonoBehaviour, IDamageableByPlayer, IDestroyableByImpact
{
    
    [SerializeField]
    private LinearMoveInDirection myMovementScript;

    public delegate void _UfoDestroyedEvent();
    public event _UfoDestroyedEvent UfoDestroyedEvent;


    public void SetMoveDirectionAndSpeed(Vector3 moveDirection, float speed)
    {
        myMovementScript.SetMoveDirection(moveDirection);
        myMovementScript.SetSpeed(speed);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDestroyableByImpact destroyableByImpact = collision.GetComponent<IDestroyableByImpact>();
        if (destroyableByImpact != null)
        {
            destroyableByImpact.DestoyedByImpact();
            DestroySelf();
        }
    }

    public void HitByPlayerProjectile()
    {
        DestroySelf();
    }

    public void DestoyedByImpact()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        if (gameObject.activeSelf)
        {
            GeneralSoundsScript.PlayExplosionSoundEffect();
            UfoDestroyedEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
