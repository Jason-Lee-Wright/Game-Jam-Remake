using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMianMenu : MonoBehaviour
{
    public GameObject MainMenu, win, lose, game, gameScreen;
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
        win.SetActive(false);
        lose.SetActive(false);
        game.SetActive(false);
        gameScreen.SetActive(false);
    }
}
