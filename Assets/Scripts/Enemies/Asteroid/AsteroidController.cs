using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamageableByPlayer, IDestroyableByImpact
{
    private float speed;
    private Vector3 movementDirection;


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
            SpawnManagerAsteroids.Instance.AsteroidDestroyed(this, true);
            gameObject.SetActive(false);
        }
    }

   

    public void DestoyedByImpact()
    {
        if (gameObject.activeSelf)
        {
            GeneralSoundsScript.PlayExplosionSoundEffect();
            SpawnManagerAsteroids.Instance.AsteroidDestroyed(this, false);
            gameObject.SetActive(false);
        }
    }

}
