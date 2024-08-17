using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DistanceChecker : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform target; // Reference to the target sprite
    public float detectionRange = 2f; // The distance to trigger the message

    private bool playerNearby = false;
    private bool enemiesDefeated = false;

    private bool messageShown = false;
    private float detectionRangeSqr;

    public GameObject hostiles;
    public TMP_Text messageText;

    public GameObject messageCanvas;
    public Image tooltipKeyboard;
    public Image tooltipXbox;

    public LevelChanger levelChanger;

    private bool tooltipVisable = false;

    void Start()
    {
        detectionRangeSqr = detectionRange * detectionRange;
    }

    void Update()
    {
        CheckEnemies();

        UpdateTooltipVisibility();

        float distanceSqr = (player.position - target.position).sqrMagnitude;

        if (distanceSqr <= detectionRangeSqr && !messageShown)
        {
            ShowMessage();
            messageShown = true;
            if (messageCanvas != null) messageCanvas.SetActive(true);

            playerNearby = true;
        }
        else if (distanceSqr > detectionRangeSqr && messageShown)
        {
            HideMessage();
            messageShown = false;
            if (messageCanvas != null) messageCanvas.SetActive(false);

            playerNearby = false;
        }

        if (tooltipVisable && enemiesDefeated && playerNearby)
        {
            if (Input.GetKeyDown("e"))
            {
                levelChanger.FadeToLevel(3);
            }
        }
    }

    private void ShowMessage()
    {
        Debug.Log("You have Entered the area!");
    }

    private void HideMessage()
    {
        Debug.Log("You have Left the area!");
    }

    private void UpdateTooltipVisibility()
    {
        if (playerNearby && enemiesDefeated)
        {
            // Enable the appropriate tooltip based on the current input device
            if (Gamepad.current != null)
            {
                ShowTooltip(tooltipXbox);
                tooltipVisable = true;
            }
            else
            {
                ShowTooltip(tooltipKeyboard);
                tooltipVisable = true;
            }
        }
        else
        {
            // Hide both tooltips if conditions are not met
            tooltipKeyboard.gameObject.SetActive(false);
            tooltipXbox.gameObject.SetActive(false);
            tooltipVisable = false;
        }
    }

    private void ShowTooltip(Image tooltip)
    {
        tooltipKeyboard.gameObject.SetActive(tooltip == tooltipKeyboard);
        tooltipXbox.gameObject.SetActive(tooltip == tooltipXbox);
    }

    private void CheckEnemies()
    {
        // Count the number of active enemy children in the Hostiles object
        int activeEnemies = 0;
        foreach (Transform enemy in hostiles.transform)
        {
            if (enemy.gameObject.activeSelf) // Check if the enemy is active
            {
                activeEnemies++;
            }
        }

        // Update the message based on the active enemy count
        if (activeEnemies == 0)
        {
            ShowMessage("Continue");
            enemiesDefeated = true;
        }
        else
        {
            enemiesDefeated = false;
            ShowMessage($"Kill all the enemies and return! {activeEnemies} left.");
        }
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }
}
