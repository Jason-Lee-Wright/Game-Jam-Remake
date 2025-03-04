using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuScreen, GameScreen, game;

    public TextMeshProUGUI levelText; // UI Text to display the selected level
    public AudioSource downChime, upChime;

    private string[] levels = { "Level 1", "Level2", "Level3", "Level4" }; // Each level in order
    private string[] levelInfo = { "The sock Puppet is dying. You should stop that. Lucky for you there are random drugs everywhere... \nthats the perfect thing to feed it!",
                                   "These pills are just allergies??? \nThat should work!!",
                                   "Drugs AND allergies....... \nWHAT A GOOD IDEA!!!!!",
                                   "These are just regular medicine.... \nfine I guess this can work.."};
    public int currentLevelIndex = 0;

    void Start()
    {
        UpdateLevelText();
        GameScreen.SetActive(false);
        game.SetActive(false);
        MenuScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            downChime.Play();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            upChime.Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Make play chime audio
            ChangeLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Make play chime audio
            ChangeLevel(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //Make play chime audio
            StartGame();
        }
    }

    void ChangeLevel(int change)
    {
        currentLevelIndex += change;
        if (currentLevelIndex >= levels.Length) currentLevelIndex = levels.Length - 1;
        if (currentLevelIndex < 0) currentLevelIndex = 0;
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        levelText.text = "Selected Level: " + levels[currentLevelIndex] + "\n" + levelInfo[currentLevelIndex];

        // Later use currentLevelIndex to change text to be more specific
    }

    void StartGame()
    {
        game.SetActive(true);

        GamemodeInt.GameSetter?.Invoke(currentLevelIndex);

        MenuScreen.SetActive(false);
        GameScreen.SetActive(true);
    }
}

public static class GamemodeInt
{
    public static Action<int> GameSetter;
}