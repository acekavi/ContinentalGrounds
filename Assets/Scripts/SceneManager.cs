using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PlayerInteractions player;  // Reference to the player GameObject
    public Canvas MainCanvas;  // Reference to the Canvas
    public ConversationsManager conversationsManager;  // Reference to the ConversationsManager

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            player.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Player GameObject not assigned.");
        }

        if (MainCanvas != null)
        {
            MainCanvas.gameObject.SetActive(false);
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("Start Menu Canvas not assigned.");
        }
    }

    public void OnStartGameButtonPressed()
    {
        if (player != null)
        {
            conversationsManager.InitialConversation();  // Start the initial conversation
        }
        else
        {
            Debug.LogError("Player GameObject not assigned.");
        }

        if (MainCanvas != null)
        {
            MainCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.LogError("Start Menu Canvas not assigned.");
        }
    }
}
