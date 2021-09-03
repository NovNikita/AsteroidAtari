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

    //value for initial size of asteroid pool, there would be generated x2 medium and x4 small ones
    private int amountOfLargeAsteroidsInStartingPool = 5;

    private int largeAsteroidsToSpawn, asteroidsCount = 0;

    private List<GameObject> smallAsteroidsPool;
    private List<GameObject> mediumAsteroidsPool;
    private List<GameObject> largeAsteroidsPool;

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



    void Start()
    {
        InitializeAsteroidPools(amountOfLargeAsteroidsInStartingPool);

        //determine play area to spawn asteroids
        spawnXMax = Camera.main.ViewportToWorldPoint(Vector3.right).x;
        spawnXMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        spawnYMax = Camera.main.ViewportToWorldPoint(Vector3.up).y;
        spawnYMin = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

    }

    private void InitializeAsteroidPools(int amountOfLargeAsteroidsInPool)
    {
        largeAsteroidsPool = ObjectPoolManager.InitializePool(largeAsteroidPf, amountOfLargeAsteroidsInPool);
        mediumAsteroidsPool = ObjectPoolManager.InitializePool(mediumAsteroidPf, amountOfLargeAsteroidsInPool * 2);
        smallAsteroidsPool = ObjectPoolManager.InitializePool(smallAsteroidPf, amountOfLargeAsteroidsInPool * 4);


        //subscribing on asteroid destroyed events
        for (int i = 0; i < amountOfLargeAsteroidsInPool; i++) largeAsteroidsPool[i].GetComponent<AsteroidController>().AsteroidDestroyed += OnAsteroidDestroyed;
        for (int i = 0; i < amountOfLargeAsteroidsInPool * 2; i++) mediumAsteroidsPool[i].GetComponent<AsteroidController>().AsteroidDestroyed += OnAsteroidDestroyed;
        for (int i = 0; i < amountOfLargeAsteroidsInPool * 4; i++) smallAsteroidsPool[i].GetComponent<AsteroidController>().AsteroidDestroyed += OnAsteroidDestroyed;
    }


    //initialization for new game
    public void StartNewGame()
    {
        StopAllCoroutines();

        foreach (GameObject asteroid in largeAsteroidsPool)
            asteroid.SetActive(false);

        foreach (GameObject asteroid in mediumAsteroidsPool)
            asteroid.SetActive(false);

        foreach (GameObject asteroid in smallAsteroidsPool)
            asteroid.SetActive(false);

        asteroidsCount = 0;

        largeAsteroidsToSpawn = StartingAmountOfAsteroids;
        SpawnLargeAsteroids(largeAsteroidsToSpawn);
    }


    private void SpawnLargeAsteroids(int amountToSpawn)
    {
        for (int i =0; i<amountToSpawn; i++)
        {

            Vector2 randomPos = new Vector2(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax));

            while (CheckIfPlayerIsNear(randomPos, 3))
            {
                randomPos = new Vector2(Random.Range(spawnXMin, spawnXMax), Random.Range(spawnYMin, spawnYMax));
            }

            GameObject asteroidToSpawn = ObjectPoolManager.RetrieveFromPool(largeAsteroidsPool);

            //Object pool returnes existing disabled asteroid if there is any, and creates and return new one if all occupied
            //In second case we need to subscribe on AsteroidDestroyed event of new asteroid object
            //-------------------------------------------------------------------------------------------
            if (asteroidToSpawn.GetComponent<AsteroidController>().CheckIfAsteroidDestroyedEventIsNull())
            {
                asteroidToSpawn.GetComponent<AsteroidController>().AsteroidDestroyed += OnAsteroidDestroyed;
            }
            //-------------------------------------------------------------------------------------------

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

            SpawnAsteroid(asteroidToSpawn, randomPos, randomDirection, randomSpeed);
       
        }
    }



    private void SpawnAsteroid(GameObject asteroidToSpawn, Vector2 positionToSpawn, Vector2 movementDirection, float speed)
    {

        asteroidToSpawn.transform.position = positionToSpawn;
        asteroidToSpawn.GetComponent<AsteroidController>().SetMovementDirection(movementDirection);
        asteroidToSpawn.GetComponent<AsteroidController>().SetSpeed(speed);
        asteroidToSpawn.SetActive(true);

        asteroidsCount++;
    }


    private bool CheckIfPlayerIsNear(Vector2 position, float radius)
    {
        Collider2D[] collidersInArea = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D collider in collidersInArea)
        {
            if (collider.gameObject.tag == "Player")
                return true;
        }
        return false;
    }


    //Method is called by asteroids getting destroyed by projectile or impact
    //first parameter - reference to asteroid being destroyed to get coordinates to spawn new ones
    //second one - if new ones need to be spawn(false in case of impact)
    private void OnAsteroidDestroyed(AsteroidController destroyedAsteroid, bool spawnSmallerOnes)
    {
        asteroidsCount--;

        switch (destroyedAsteroid.tag)
        {

            case "Asteroid_Small":
                break;


            case "Asteroid_Medium":
                if (spawnSmallerOnes)
                {
                    float randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

                    //spawn two asteroid +-45 degrees relative to original asteroid movement direction
                    GameObject asteroidToSpawn = ObjectPoolManager.RetrieveFromPool(smallAsteroidsPool);
                    SpawnAsteroid(asteroidToSpawn, destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, 45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);

                    asteroidToSpawn = ObjectPoolManager.RetrieveFromPool(smallAsteroidsPool);
                    SpawnAsteroid(asteroidToSpawn, destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, -45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);
                }
                break;


            case "Asteroid_Large":
                if (spawnSmallerOnes)
                {
                    float randomSpeed = Random.Range(minAsteroidSpeed, maxAsteroidSpeed);

                    //spawn two asteroid +-45 degrees relative to original asteroid movement direction
                    GameObject asteroidToSpawn = ObjectPoolManager.RetrieveFromPool(mediumAsteroidsPool);
                    SpawnAsteroid(asteroidToSpawn, destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, 45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);
                    
                    asteroidToSpawn = ObjectPoolManager.RetrieveFromPool(mediumAsteroidsPool);
                    SpawnAsteroid(asteroidToSpawn, destroyedAsteroid.transform.position, Quaternion.Euler(0, 0, -45) * destroyedAsteroid.GetMovementDirection(), randomSpeed);
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

    IEnumerator  WaitToSpawnNewWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        SpawnLargeAsteroids(largeAsteroidsToSpawn);
    }





}
