using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    [SerializeField] 
    private float projectileSpeed = 4.5f;

    private float projectileLifeTime;

    void Awake()
    {
        //determing lifetime by dividing screen width in world units by projectile speed
        projectileLifeTime = (Camera.main.ViewportToWorldPoint(Vector3.right).x - Camera.main.ViewportToWorldPoint(Vector3.zero).x) / projectileSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime); //moving forward
    }


    private void OnEnable()
    {
        StartCoroutine(DeactivationTimer()); 
    }

    IEnumerator DeactivationTimer()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        gameObject.SetActive(false);
    }

}
