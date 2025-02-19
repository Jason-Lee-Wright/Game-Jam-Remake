using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    public Text levelText; // UI Text to display the selected level
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
            ChangeLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLevel(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void ChangeLevel(int change)
    {
        currentLevelIndex += change;
        if (currentLevelIndex >= levels.Length) currentLevelIndex = 0;
        if (currentLevelIndex < 0) currentLevelIndex = levels.Length - 1;
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
