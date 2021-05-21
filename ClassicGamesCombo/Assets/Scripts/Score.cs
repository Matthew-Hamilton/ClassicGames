using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject ScoreTextGO;
    Text ScoreText;

    int GameScore;

    // Start is called before the first frame update
    void Start()
    {
        GameScore = 0;
        ScoreText = ScoreTextGO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int Points)
    {
        GameScore += Points;
        ScoreText.text = "Score: " + GameScore.ToString();
    }

    public void SetScore(int Points)
    {
        GameScore = Points;
        transform.gameObject.GetComponent<Text>().text = "Score: " + Points.ToString();
    }

    public int GetScore()
    {
        return GameScore;
    }
}
