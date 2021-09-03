using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script to re-spawn player after death, keeping track of lifes remaining
public class SpawnManagerPlayer : MonoBehaviour
{
    [SerializeField]
    private int maxLifes = 3;

    [SerializeField]
    private float timeToSpawnPlayer = 3;

    [SerializeField]
    private float invincibleTime = 3;

    [SerializeField]
    private PlayerMainScript player;

    [SerializeField]
    private LifesLeftDisplayScript lifesLeftDisplayScript;

    private int lifesLeft;

    private void Start()
    {
        lifesLeftDisplayScript.InitializeLifesDisplay(maxLifes);
        player.PlayerKilledEvent += OnPlayerKilled;
    }

    public void StartNewGame()
    {
        lifesLeft = maxLifes;
        lifesLeftDisplayScript.ChangeLifesDisplay(lifesLeft);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player.gameObject.SetActive(true);
        player.transform.position = Vector3.zero;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.SetInvincible(invincibleTime);
    }

    private void OnPlayerKilled()
    {
        lifesLeft--;
        lifesLeftDisplayScript.ChangeLifesDisplay(lifesLeft);

        if (lifesLeft > 0)
        Invoke("SpawnPlayer", timeToSpawnPlayer);
    }





}
