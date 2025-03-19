using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMianMenu : MonoBehaviour
{
    public GameObject MainMenu, game, GameOver, gameScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainMenu.SetActive(true);
            DeactivateObjects();
        }
    }

    void DeactivateObjects()
    {
        GameOver.SetActive(false);
        game.SetActive(false);
        gameScreen.SetActive(false);
    }
}
