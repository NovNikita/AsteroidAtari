using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class managing spawn of asteroids
public class SpawnManagerAsteroids : MonoBehaviour
{
    //coordinate limits to spawn asteroids, calculated during initialization
    private float spawnXMin, spawnXMax, spawnYMin, spawnYMax;

    //pause before next wave
    private float timeBetweenWaves = 2;

    private int largeAsteroidsToSpawn, asteroidsCount = 0;

    public static SpawnManagerAsteroids Instance { private set; get; }

    [SerializeField]
    private int StartingAmountOfAsteroids = 2;

    [SerializeField] 
    private float minAsteroidSpeed = 0.5f, maxAsteroidSpeed = 2;

    [SerializeField] 
    private GameObject smallAsteroidPf;

    [SerializeField] 
    private GameObject mediumAsteroidPf;

    [SerializeField] 
    private GameObject largeAsteroidPf;

    private void Awake()
    {
        if (SpawnManagerAsteroids.Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        //determine play area to spawn asteroids
        spawnXMax = Camera.main.ViewportToWorldPoint(Vector3.right).x;
        spawnXMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        spawnYMax = Camera.main.ViewportToWorldPoint(Vector3.up).y;
        spawnYMin = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

    }


    //initialization for new game
    public void StartNewGame()
    {
        StopAllCoroutines();

        asteroidsCount = 0;

        largeAsteroidsToSpawn = StartingAmountOfAsteroids;
        SpawnLargeAsteroids(largeAsteroidsToSpawn);
    }


    private void SpawnLargeAsteroids(int amountToSpawn)
    {
        for (int i =0; i<amountToSpawn; i++)
        {

            Vector2 randomPos = new Vector2(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax));

            Vector2 playerToRandomPosVector = randomPos - (Vector2)PlayerMainScript.Instance.transform.position;
            if (playerToRandomPosVector.magnitude < 3)
            {
                playerToRandomPosVector.Normalize();
                playerToRandomPosVector *= 3;
                randomPos = (Vector2)PlayerMainScript.Instance.transform.position + playerToRandomPosVector;
            }

            GameObject asteroidToSpawn = PoolManager.GetObject(largeAsteroidPf);

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

            SpawnAsteroid(asteroidToSpawn.GetComponent<AsteroidController>(), randomPos, randomDirection, randomSpeed);
       
        }
    }



    private void SpawnAsteroid(AsteroidController asteroidToSpawn, Vector2 positionToSpawn, Vector2 movementDirection, float speed)
    {

        asteroidToSpawn.transform.position = positionToSpawn;
        asteroidToSpawn.SetMovementDirection(movementDirection);
        asteroidToSpawn.SetSpeed(speed);
        asteroidToSpawn.gameObject.SetActive(true);

        asteroidsCount++;
    }



    //Method is called by asteroids getting destroyed by projectile or impact
    //first parameter - reference to asteroid being destroyed to get coordinates to spawn new ones
    //second one - if new ones need to be spawn(false in case of impact)
    public void AsteroidDestroyed(AsteroidController destroyedAsteroid, bool spawnSmallerOnes)
    {
        asteroidsCount--;

        switch (destroyedAsteroid.tag)
        {

            case "Asteroid_Small":
                break;


            case "Asteroid_Medium":
                if (spawnSmallerOnes)
                {
                    SpawnSmallerAsteroids(destroyedAsteroid, smallAsteroidPf);
                }
                break;


            case "Asteroid_Large":
                if (spawnSmallerOnes)
                {
                    SpawnSmallerAsteroids(destroyedAsteroid, mediumAsteroidPf);
                }
                break;


            default:
                Debug.Log("Added asteroid have incorrect tag: " + destroyedAsteroid.name);
                break;

                
        }


        if (asteroidsCount < 1)
        {
            largeAsteroidsToSpawn++;
            StartCoroutine(WaitToSpawnNewWave());
        }

    }

    //Method to spawn two asteroid +-45 degrees relative to original asteroid movement direction
    private void SpawnSmallerAsteroids(AsteroidController destroyedAsteroid, GameObject asteroidPrefabToSpawn)
    {
        float randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

        GameObject asteroidToSpawn = PoolManager.GetObject(asteroidPrefabToSpawn);
        SpawnAsteroid(asteroidToSpawn.GetComponent<AsteroidController>(), destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, 45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);

        asteroidToSpawn = PoolManager.GetObject(asteroidPrefabToSpawn);
        SpawnAsteroid(asteroidToSpawn.GetComponent<AsteroidController>(), destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, -45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);
    }


    IEnumerator  WaitToSpawnNewWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        SpawnLargeAsteroids(largeAsteroidsToSpawn);
    }





}
