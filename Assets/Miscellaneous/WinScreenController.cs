using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    // References to health bars
    public Slider player1HealthSlider;
    public Slider player2HealthSlider;

    // Win screen images
    public Image player1WinImage;
    public Image player2WinImage;

    // UI Text for round announcements
    public Text roundAnnouncementText;

    // Game variables
    private int player1Wins = 0;
    private int player2Wins = 0;
    private int currentRound = 1;
    private int totalRounds = 3;

    private bool roundActive = true;

    void Start()
    {
        StartCoroutine(AnnounceRound());
    }

    void Update()
    {
        if (roundActive)
        {
            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if (player1HealthSlider.value <= 0 && roundActive)
        {
            roundActive = false;
            player2Wins++;
            ShowWinScreen(2);
        }
        else if (player2HealthSlider.value <= 0 && roundActive)
        {
            roundActive = false;
            player1Wins++;
            ShowWinScreen(1);
        }
    }

    void ShowWinScreen(int winningPlayer)
    {
        if (winningPlayer == 1)
        {
            player1WinImage.enabled = true;
            player2WinImage.enabled = false;
        }
        else if (winningPlayer == 2)
        {
            player1WinImage.enabled = false;
            player2WinImage.enabled = true;
        }

        StartCoroutine(HandleEndOfRound());
    }

    IEnumerator AnnounceRound()
    {
        roundAnnouncementText.text = "Round " + currentRound + "!";
        roundAnnouncementText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // Show round announcement for 3 seconds
        roundAnnouncementText.gameObject.SetActive(false);
    }

    IEnumerator HandleEndOfRound()
    {
        yield return new WaitForSeconds(5f); // Wait 5 seconds on the win screen
        player1WinImage.enabled = false;
        player2WinImage.enabled = false;

        if (currentRound >= totalRounds || player1Wins > totalRounds / 2 || player2Wins > totalRounds / 2)
        {
            // If the game is over, show the final winner and transition to the Game Over screen
            ShowFinalWinner();
        }
        else
        {
            // Start the next round
            currentRound++;
            roundActive = true;
            ResetHealthBars();
            StartCoroutine(AnnounceRound());
        }
    }

    void ShowFinalWinner()
    {
        if (player1Wins > player2Wins)
        {
            player1WinImage.enabled = true;
        }
        else if (player2Wins > player1Wins)
        {
            player2WinImage.enabled = true;
        }

        StartCoroutine(SwitchToGameOverScreen());
    }

    IEnumerator SwitchToGameOverScreen()
    {
        yield return new WaitForSeconds(10f); // Wait 10 seconds
        SceneManager.LoadScene("GameOverScene");
    }

    void ResetHealthBars()
    {
        player1HealthSlider.value = player1HealthSlider.maxValue;
        player2HealthSlider.value = player2HealthSlider.maxValue;
    }
}
