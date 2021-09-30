using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject projectilePf;

    [SerializeField]
    private float shootCoolDownMin, shootCoolDownMax;

    float projectileSpawnOffset = 0.15f;

    private Transform player;

    private AudioSource shootSoundAS;

    private void Start()
    {
        shootSoundAS = GetComponent<AudioSource>();

        projectilePf = Resources.Load("Prefabs/ProjectileUFO") as GameObject;
    }


    private void OnEnable()
    {
        //Start timer to shoot at player
        StartCoroutine(CountDownToShoot(Random.Range(shootCoolDownMin, shootCoolDownMax)));
    }


    IEnumerator CountDownToShoot(float timeToShoot)
    {
        yield return new WaitForSeconds(timeToShoot);
        ShootAtPlayer();
        StartCoroutine(CountDownToShoot(Random.Range(shootCoolDownMin, shootCoolDownMax)));
    }


    private void ShootAtPlayer()
    {
        //---------------------------------------------------------
        //this part finds player before first shot(or if player reference was nulled after) and stores it for later
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (player == null)
                return;
        }

        //---------------------------------------------------------


        GameObject projectile = PoolManager.GetObject(projectilePf);

        //determine rotation
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        projectile.transform.rotation = rotation;

        //set position
        projectile.transform.position = transform.position + (projectile.transform.rotation * Vector3.up * projectileSpawnOffset);

        projectile.SetActive(true);

        shootSoundAS.Play();
    }
}
