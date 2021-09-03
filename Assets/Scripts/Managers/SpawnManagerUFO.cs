using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling spawn and re-spawn of UFO enemy
public class SpawnManagerUFO : MonoBehaviour
{

    [SerializeField] 
    private float timeToSpawnUfoMin, timeToSpawnUfoMax;

    [SerializeField] 
    private UFOController ufo;

    private float ufoSpeed;
    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
        ufo.UfoDestroyedEvent += OnUfoDestroyed;
    }

    public void StartNewGame()
    {
        StopAllCoroutines();
        ufo.gameObject.SetActive(false);
        StartCoroutine(TimerToSpawnUFO(Random.Range(timeToSpawnUfoMin, timeToSpawnUfoMax)));
    }

    IEnumerator TimerToSpawnUFO(float timeToSpawnUFO)
    {
        yield return new WaitForSeconds(timeToSpawnUFO);
        SpawnUFO();
    }

    private void SpawnUFO()
    {
        //randomize UFO speed(time to cross screen: 8.5 - 11.5 seconds)
        ufoSpeed = (Camera.main.ViewportToWorldPoint(Vector3.right).x - Camera.main.ViewportToWorldPoint(Vector3.zero).x) / (10 + Random.Range(-1.5f, 1.5f));


        //randomize side to spawn and determine direction to move
        float posX;
        if (Random.Range(0,2) == 0)
        {
            posX = mainCamera.ViewportToWorldPoint(Vector3.zero).x;
            ufo.SetMoveDirectionAndSpeed(Vector3.right, ufoSpeed);
        }
        else
        {
            posX = mainCamera.ViewportToWorldPoint(Vector3.right).x;
            ufo.SetMoveDirectionAndSpeed(Vector3.left, ufoSpeed);
        }

        //randomize Y coordinate to spawn
        float posY = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0.2f, 0)).y, mainCamera.ViewportToWorldPoint(new Vector3(0, 0.8f, 0)).y);

        //set position
        ufo.transform.position = new Vector3(posX, posY, 0);

        ufo.gameObject.SetActive(true);

    }

    private void OnUfoDestroyed()
    {
        StartCoroutine(TimerToSpawnUFO(Random.Range(timeToSpawnUfoMin, timeToSpawnUfoMax)));
    }
}
