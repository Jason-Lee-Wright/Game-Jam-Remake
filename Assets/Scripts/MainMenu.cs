using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI levelText; // UI Text to display the selected level
    public AudioClip downChime, upChime, spaceChime, moveBottle, safeBottle, deathBottle;

    private string[] levels = { "Level1", "Level2", "Level3", "Level4" }; // Each level in order
    private int currentLevelIndex = 0;

    void Start()
    {
        UpdateLevelText();
    }

    void Update()
    {
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
        levelText.text = "Selected Level: " + levels[currentLevelIndex];

        // Later use currentLevelIndex to change text to be more specific
    }

    void StartGame()
    {
        SceneManager.LoadScene(levels[currentLevelIndex]);
    }
}
