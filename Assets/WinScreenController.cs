using System.Collections;
using System.Collections.Generic;
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

    private bool gameEnded = false;

    void Update()
    {
        if (!gameEnded)
        {
            CheckHealth();
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
        }
        else if (winningPlayer == 2)
        {
            player1WinImage.enabled = false;
            player2WinImage.enabled = true;
        }
    }
}
