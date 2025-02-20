using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class PlayGame : MonoBehaviour
{
    public TextMeshProUGUI bottleText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playerTime;
    public float gameTime = 60f;
    private float currentTime;

    private string[] statements = { "Tastes like chicken!", "Smells funny...", "Guaranteed to kill!", "Sweet and harmless", "Death", "Looks healthy!", "Might be expired...", "Glows in the dark" };
    private string[] deathStatements = { "Guaranteed to kill!", "Death" };
    private List<string> eatenItems = new List<string>();

    private float playerTimeCount = 20f;

    void Start()
    {
        currentTime = gameTime;
        SelectNewBottle();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        playerTime.text = "Time: " + Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0)
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) // Eat
        {
            EatBottle();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Don't eat
        {
            SelectNewBottle();
        }
    }

    void SelectNewBottle()
    {
        //use left and right arrows to select bottles. Display what is written on the bottles
    }

    void EatBottle()
    {
        //controll waether you gain time or die
    }

    void LoseGame()
    {
        Debug.Log("You lost! Items eaten: " + string.Join(", ", eatenItems));
        SceneManager.LoadScene("GameOver");
    }

    void WinGame()
    {
        Debug.Log("You won! Items eaten: " + string.Join(", ", eatenItems));
        SceneManager.LoadScene("WinScene");
    }
}