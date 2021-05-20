using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{

    public GameObject LivesGO;
    Text LivesText;

    public int numLives = 3;
    bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (numLives <= 0)
        {
            endGame = true;
            return;
        }
        LivesText.text = "LIVES: ";
        for(int i = 0; i <numLives; i++)
        {
            LivesText.text += "I ";
        }
    }
}
