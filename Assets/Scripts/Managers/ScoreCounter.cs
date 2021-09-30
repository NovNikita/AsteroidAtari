using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Class to keep score count done with singletop pattern

public class ScoreCounter : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public static ScoreCounter Instance { private set; get;  }

    private int score = 0;

    [SerializeField] private ScoreValueStruct[] scoreValue; 

    [System.Serializable]
    private struct ScoreValueStruct
    {
        public string killedObjectTag;
        public int scoreValue;
    }

    private void Awake()
    {
        if (SpawnManagerAsteroids.Instance == null)
            Instance = this;
        else
            Destroy(this);
    }


    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(string objectDestroyedTag)
    {
        int i = 0;
        while (i < scoreValue.Length)
        {
            if (scoreValue[i].killedObjectTag == objectDestroyedTag)
            {
                score += scoreValue[i].scoreValue;
                break;
            }
            i++;
        }

        if (i == scoreValue.Length)
            Debug.Log("Destroyed Object's Tag does not represented in ScoreValues list");

        scoreText.text = score.ToString();
    }





}
