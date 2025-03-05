using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public enum GameMode { Bottle, Food, Both, LessTime }
    public GameMode gameMode;

    public AudioSource moveBottle, safeBottle, deathBottle;

    public TextMeshProUGUI itemText, timerText, playerTime, WinItems, LoseItems;
    public GameObject LoseScreen, WinScreen, GameScreen;
    public float gameTime = 60f;
    private float currentTime;
    private float PlayerTimeRemaining;

    private string[] foods = { "Peanuts", "Strawberries", "Shellfish", "Milk", "Eggs", "Soy", "Wheat", "Fish", "Tree Nuts" };
    private string[] statements = { "Tastes like chicken", "Smells funny", "Guaranteed to kill", "Sweet and harmless", "Death", "Looks healthy", "Might be expired", "Glows in the dark" };
    private string[] deathStatements = { "Guaranteed to kill", "Death" };

    private List<string> allergicFoods = new List<string>();
    private List<string> eatenItems = new List<string>();
    private int killBottleCount = 0;
    private List<string> currentStatements = new List<string>();
    private string currentItem;

    private bool GameOverNow;

    public MainMenu mainMenu;

    void OnEnable()
    {
        // Subscribe to the delegate
        GamemodeInt.GameSetter += SetGameMode;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        GamemodeInt.GameSetter -= SetGameMode;
    }

    void SetGameMode(int levelIndex)
    {
        switch (levelIndex)
        {
            case 0: gameMode = GameMode.Bottle; break;
            case 1: gameMode = GameMode.Food; break;
            case 2: gameMode = GameMode.Both; break;
            case 3: gameMode = GameMode.LessTime; break;
        }

        resetGame();

        Debug.Log("Game Mode Set To: " + gameMode);
    }

    void Awake()
    {

        currentTime = gameTime;

        gameTime = 60f;

        if (gameMode == GameMode.LessTime)
        {
            PlayerTimeRemaining = currentTime / 8;
        }
        else
        {
            PlayerTimeRemaining = currentTime / 6;
        }

        SelectNewItem();

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    void resetGame()
    {
        currentTime = gameTime;

        gameTime = 60f;

        if (gameMode == GameMode.LessTime)
        {
            PlayerTimeRemaining = currentTime / 8;
        }
        else
        {
            PlayerTimeRemaining = currentTime / 6;
        }

        itemText.text = string.Empty;

        SelectNewItem();

        LoseItems.text = string.Empty;
        WinItems.text = string.Empty;
        allergicFoods.Clear();
        eatenItems.Clear();
        currentStatements.Clear();

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);

        GameOverNow = false;
    }

    void Update()
    {
        if (GameOverNow == false)
        {
            currentTime -= Time.deltaTime;
            PlayerTimeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
            playerTime.text = "Time: " + Mathf.Ceil(PlayerTimeRemaining).ToString();
        }


        if (currentTime <= PlayerTimeRemaining)
        {
            WinGame();
        }
        else if (0 >= PlayerTimeRemaining)
        {
            LoseGame();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Eat/Drink
        {
            ConsumeItem();
            moveBottle.Play();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Skip
        {
            SelectNewItem();
            moveBottle.Play();
        }
    }

    void SelectNewItem()
    {
        itemText.text = string.Empty;

        if (gameMode != GameMode.Bottle && gameMode != GameMode.LessTime)
        {
            currentItem = foods[Random.Range(0, foods.Length)];
            itemText.text = "Food: " + currentItem + "\nAllergies: " + string.Join("  ", allergicFoods);
        }

        if (gameMode != GameMode.Food)
        {
            currentStatements.Clear();

            if (killBottleCount >= 3)
            {
                currentStatements.Add("Sweet and harmless"); // Guaranteed safe bottle
                killBottleCount = 0;
            }
            else
            {
                int numStatements = Random.Range(2, 4);
                for (int i = 0; i < numStatements; i++)
                {
                    string newStatement = statements[Random.Range(0, statements.Length)];
                    if (!currentStatements.Contains(newStatement))
                    {
                        currentStatements.Add(newStatement);
                    }
                }

                if (currentStatements.Any(statement => deathStatements.Contains(statement)))
                {
                    killBottleCount++;
                }
            }

            itemText.text += "\nBottle says:\n" + string.Join("\n", currentStatements);
        }
    }

    void ConsumeItem()
    {
        if (gameMode == GameMode.Food || gameMode == GameMode.Both)
        {
            eatenItems.Add(currentItem);
            if (allergicFoods.Contains(currentItem))
            {
                deathBottle.Play();
                LoseGame();
                return;
            }
            else
            {
                if (Random.value < 0.5f)
                {
                    allergicFoods.Add(currentItem);
                }
                PlayerTimeRemaining += 12f;
            }
        }

        if (gameMode == GameMode.Bottle || gameMode == GameMode.Both || gameMode == GameMode.LessTime)
        {
            eatenItems.AddRange(currentStatements);
            if (currentStatements.Any(statement => deathStatements.Contains(statement)))
            {
                deathBottle.Play();
                LoseGame();
                return;
            }
            else
            {
                if (gameMode == GameMode.LessTime)
                {
                    PlayerTimeRemaining += 6f;
                }
                else
                {
                    PlayerTimeRemaining += 12f;
                }
            }
        }

        safeBottle.Play();
        SelectNewItem();
    }

    void LoseGame()
    {
        GameOverNow = true;
        LoseItems.text = "You lost! Items consumed: " + string.Join(", ", eatenItems);
        LoseScreen.SetActive(true);
        GameScreen.SetActive(false);
    }

    void WinGame()
    {
        GameOverNow = true;
        WinItems.text = "You won! Items consumed: " + string.Join(", ", eatenItems);
        WinScreen.SetActive(true);
        PlayerTimeRemaining = 10f;
        currentTime = 60f;
        GameScreen.SetActive(false);
    }
}