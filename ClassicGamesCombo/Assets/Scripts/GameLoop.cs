using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public GameObject ControllerGO;
    public GameObject ScoreGO;
    public GameObject World;
    Lives lives;
    Score score;
    WaveSpawn waveSpawn;

    public GameObject GameInfoText;
    public GameObject RoundIntroText;
    public GameObject BigTextGO;
    Text BigText;
    public GameObject ScoreTextGO;
    Text ScoreText;

    [Range(1,5)]
    public int StartingLives = 3;


    bool runGame = false;

    // Start is called before the first frame update
    void Start()
    {
        BigText = BigTextGO.GetComponent<Text>();
        ScoreText = ScoreTextGO.GetComponent<Text>();
        GameInfoText.active = false;
        RoundIntroText.active = false;

        World.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!runGame)
        {
            if(Input.anyKeyDown)
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {

        Score score = ScoreGO.GetComponent<Score>();
        Lives lives = ControllerGO.GetComponent<Lives>();
        WaveSpawn waveSpawn = ControllerGO.GetComponent<WaveSpawn>();

        BigTextGO.transform.parent.gameObject.active = false;
        GameInfoText.active = true;
        RoundIntroText.active = true;
        ScoreTextGO.active = false;
        score.SetScore(0);
        lives.SetLives(StartingLives);
        waveSpawn.Restart();

        World.active = true;
        runGame = true;

    }

    public void EndGame()
    {
        Score score = ScoreGO.GetComponent<Score>();

        foreach (Enemy enemy in ControllerGO.transform.parent.GetComponentsInChildren<Enemy>())
        {
            Destroy(enemy.transform.gameObject);
        }
        foreach (Bullet bullet in ControllerGO.transform.parent.GetComponentsInChildren<Bullet>())
        {
            Destroy(bullet.transform.gameObject);
        }

        BigTextGO.transform.parent.gameObject.active = true;
        GameInfoText.active = false;
        RoundIntroText.active = false;
        ScoreTextGO.active = true;

        ScoreTextGO.GetComponent<Text>().text = "SCORE: " + score.GetScore();
        BigText.text = "GAME OVER";

        World.active = false;
        runGame = false;
        Debug.Log("EndGame");
    }
}
