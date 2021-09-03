using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamageableByPlayer, IDestroyableByImpact
{
    private float speed;
    private Vector3 movementDirection;

    public delegate void _AsteroidDestroyed(AsteroidController destroyedAsteroid, bool spawnSmallerOnes);
    public event _AsteroidDestroyed AsteroidDestroyed;


    private void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetMovementDirection(Vector3 movDirection)
    {
        movementDirection = movDirection.normalized;
    }

    public Vector3 GetMovementDirection()
    {
        return movementDirection;
    }

    public void HitByPlayerProjectile()
    {
        if (gameObject.activeSelf)
        {
            GeneralSoundsScript.PlayExplosionSoundEffect();
            AsteroidDestroyed?.Invoke(this, true);
            gameObject.SetActive(false);
        }
    }

   

    public void DestoyedByImpact()
    {
        if (gameObject.activeSelf)
        {
            GeneralSoundsScript.PlayExplosionSoundEffect();
            AsteroidDestroyed?.Invoke(this, false);
            gameObject.SetActive(false);
        }
    }

    public bool CheckIfAsteroidDestroyedEventIsNull()
    {
        if (AsteroidDestroyed == null)
            return true;
        else
            return false;
    }
}
