using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Reference to Player 1 and Player 2 health bars
    public Slider player1HealthSlider;  // Use this if your health bar is a Slider
    public Slider player2HealthSlider;  // Use this if your health bar is a Slider

    public Image player1HealthImage;    // Use this if your health bar is an Image
    public Image player2HealthImage;    // Use this if your health bar is an Image

    // Win screen images
    public Image player1WinImage;
    public Image player2WinImage;

    // Game Over screen
    public GameObject gameOverPanel;    // Panel for the Game Over screen
    public Text gameOverText;           // Optional: Text displayed on the Game Over screen
    public GameObject matchOverScreen;
    public GameObject gamePlayScreen;

    private bool gameEnded = false;

    void Update()
    {

        if (!gameEnded)
        {
            CheckHealth();
        }
        else{
            if (GameManager.gameManager.p1WinsNum >= 2){MatchOver();}
            if (GameManager.gameManager.p2WinsNum >= 2){MatchOver();}
        }
    }

    void CheckHealth()
    {
        bool player1HealthZero = false;
        bool player2HealthZero = false;

        // Check health based on the component type (Slider or Image)
        if (player1HealthSlider != null)
        {
            player1HealthZero = player1HealthSlider.value <= 0;
        }
        else if (player1HealthImage != null)
        {
            player1HealthZero = player1HealthImage.fillAmount <= 0;
        }

        if (player2HealthSlider != null)
        {
            player2HealthZero = player2HealthSlider.value <= 0;
        }
        else if (player2HealthImage != null)
        {
            player2HealthZero = player2HealthImage.fillAmount <= 0;
        }

        // Show the win screen based on whose health is zero
        if (player1HealthZero)
        {
            ShowWinScreen(2); // Player 2 wins
        }
        else if (player2HealthZero)
        {
            ShowWinScreen(1); // Player 1 wins
        }
    }

    void ShowWinScreen(int winningPlayer)
    {
        gameEnded = true;
        
        if (winningPlayer == 1)
        {
            player1WinImage.enabled = true;
            player2WinImage.enabled = false;
            GameManager.gameManager.p1WinsNum++;
        }
        else if (winningPlayer == 2)
        {
            player1WinImage.enabled = false;
            player2WinImage.enabled = true;
            GameManager.gameManager.p2WinsNum++;
        }

        // Start the coroutine to show the Game Over screen
        StartCoroutine(ShowGameOverScreenAfterDelay());
    }

    IEnumerator ShowGameOverScreenAfterDelay()
    {
        // Wait for 15 seconds
        yield return new WaitForSeconds(3f);

        // Hide the win screens
        player1WinImage.enabled = false;
        player2WinImage.enabled = false;

        // Display the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Optional: Update Game Over text
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
        }
    }

    public void MatchOver(){
        matchOverScreen.SetActive(true);
        gamePlayScreen.SetActive(false);
    }
}




