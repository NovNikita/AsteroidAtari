using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : ProjectileBaseClass
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageableByPlayer>()!=null)
        {
            collision.GetComponent<IDamageableByPlayer>().HitByPlayerProjectile();
            ScoreCounter.Instance.AddScore(collision.tag);
            gameObject.SetActive(false);
        }
    }
}
