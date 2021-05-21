using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    public GameObject LivesGO;
    Text LivesText;

    public GameObject GameLoopGO;
    GameLoop GameLoopController;

    public int numLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        GameLoopController = GameLoopGO.GetComponent<GameLoop>();
        LivesText = LivesGO.GetComponent<Text>();
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RemoveLife()
    {
        numLives--;
        UpdateLives();
    }

    void UpdateLives()
    {

        LivesGO.GetComponent<Text>().text = "LIVES: ";
        for (int i = 0; i < numLives; i++)
        {
            LivesGO.GetComponent<Text>().text += "I ";
        }

        if (numLives <= 0)
        {
            Debug.Log("Should call endGame");
            GameLoopController.EndGame();
            return;
        }
    }

    public void SetLives(int _numLives)
    {
        numLives = _numLives;
        UpdateLives();
    }
}
