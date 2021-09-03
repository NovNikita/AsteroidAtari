using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUFO : ProjectileBaseClass
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMainScript>().HitByEnemy();
            gameObject.SetActive(false);
        }
    }
}
