using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class PlayGame : MonoBehaviour
{
    public AudioClip moveBottle, safeBottle, deathBottle;

    public TextMeshProUGUI bottleText, timerText, playerTime, WinItems, LoseItems;
    public GameObject LoseScreen, WinScreen, GameScreen;
    public float gameTime = 60f;
    private float currentTime;
    private float PlayerTimeRemaining;

    private string[] statements = { "Tastes like chicken", "Smells funny", "Guaranteed to kill", "Sweet and harmless", "Death", "Looks healthy", "Might be expired", "Glows in the dark" };
    private string[] deathStatements = { "Guaranteed to kill", "Death" };
    private List<string> eatenItems = new List<string>();

    private int killBottleCount = 0;
    private List<string> currentStatements = new List<string>();

    private string LevelSelected;

    void Start()
    {
        currentTime = gameTime;
        PlayerTimeRemaining = currentTime / 6;
        SelectNewBottle();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        PlayerTimeRemaining -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        playerTime.text = "Time: " + Mathf.Ceil(PlayerTimeRemaining).ToString();

        if (currentTime <= PlayerTimeRemaining)
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Eat
        {
            EatBottle();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Don't eat
        {
            SelectNewBottle();
        }
    }

    void SelectNewBottle()
    {
        currentStatements.Clear();

        if (killBottleCount >= 3)
        {
            currentStatements.Add("Sweet and harmless"); // Guaranteed live bottle
            killBottleCount = 0;
        }
        else
        {
            int numStatements = Random.Range(2, 4); // Choose 2 or 3 statements per bottle
            for (int i = 0; i < numStatements; i++)
            {
                string newStatement = statements[Random.Range(0, statements.Length)];
                if (!currentStatements.Contains(newStatement)) // Avoid duplicates
                {
                    currentStatements.Add(newStatement);
                }
            }

            if (currentStatements.Any(Statement => deathStatements.Contains(Statement)))
            {
                killBottleCount++;
            }
        }

        bottleText.text = "Bottle says:\n" + string.Join("\n", currentStatements);

    }

    void EatBottle()
    {
        eatenItems.AddRange(currentStatements);
        if (currentStatements.Any(Statement => deathStatements.Contains(Statement)))
        {
            LoseGame();
        }
        else
        {
            PlayerTimeRemaining += 12f;
            SelectNewBottle();
        }

    }

    void LoseGame()
    {
        LoseItems.text = ("You lost! Items eaten: " + string.Join(", ", eatenItems));
        LoseScreen.SetActive(true);
        GameScreen.SetActive(false);
    }

    void WinGame()
    {
        WinItems.text = ("You lost! Items eaten: " + string.Join(", ", eatenItems));
        WinScreen.SetActive(true);
        GameScreen.SetActive(false);

    }
}