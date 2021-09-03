using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultWeapon : PlayerWeaponBaseClass
{
    private float shootCooldown = 0.33f;

    private bool cooldown = false;

    //offset to spawn projectile a bit off player
    private float projectileSpawnOffset = 0.15f;

    private AudioSource shootAudioSource;

    private List<GameObject> projectilePool;

    private GameObject projectilePf;


    public void Start()
    {
        projectilePf = Resources.Load("Prefabs/PlayerDefaultProjectile") as GameObject;

        shootAudioSource = transform.Find("ShootSound").GetComponent<AudioSource>();

        projectilePool = ObjectPoolManager.InitializePool(projectilePf, 10);
    }



    public override void Shoot()
    {
        //timescale check need for not triggering shoting while in main menu
        if (!cooldown && Time.timeScale != 0)
        {

            GameObject projectile = ObjectPoolManager.RetrieveFromPool(projectilePool);

            projectile.transform.position = transform.position + (transform.rotation * new Vector3(0, projectileSpawnOffset, 0));
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);

            shootAudioSource.Play();

            cooldown = true;
            StartCoroutine(CooldownTimer());
        }

    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(shootCooldown);
        cooldown = false;
    }

    //below required to avoid infinite cooldown after death
    private void OnEnable()
    {
        cooldown = false;
    }
}
