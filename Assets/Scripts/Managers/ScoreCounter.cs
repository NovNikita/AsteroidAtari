using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Class to keep score count done with singletop pattern

public class ScoreCounter : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public static ScoreCounter instance;

    private int score = 0;


    //score values assigned depending on tag of destroyed object 

    private Dictionary<string, int> scoreValues = new Dictionary<string, int> 
    { 
        { "UFO", 200 }, 
        { "Asteroid_Small", 20 },
        { "Asteroid_Medium", 50 },
        { "Asteroid_Large", 100 }
    };


    private void Start()
    {
        if (ScoreCounter.instance == null)
            ScoreCounter.instance = this;
        else
            Destroy(this);
    }


    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore(string objectDestroyed)
    {
        if (scoreValues.ContainsKey(objectDestroyed))
            score += scoreValues[objectDestroyed];

        else 
            Debug.Log("Object with tag '" + objectDestroyed + "' is not represented in ScoreValues dictionaty");

        scoreText.text = score.ToString();
    }





}
