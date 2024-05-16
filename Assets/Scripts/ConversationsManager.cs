using TMPro;
using UnityEngine;
using DialogueEditor;
using StarterAssets;
using Unity.VisualScripting;

public class ConversationsManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private NPCConversation initialConversation;
    [SerializeField] private PlayerInteractions playerInteractions;

    void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ConversationManager.Instance.SelectNextOption();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ConversationManager.Instance.SelectPreviousOption();
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
        else
        {
            EndConversation();
        }
    }

    public void InitialConversation()
    {
        StartConversation(initialConversation);
    }

    public void ShowInteractionText(string text)
    {
        interactionText.GetComponent<TextMeshProUGUI>().text = text;
        interactionText.SetActive(true);
    }

    public void HideInteractionText()
    {
        interactionText.SetActive(false);
    }

    public void StartConversation(NPCConversation thisConversation)
    {
        // Disable movement when conversation starts
        playerInteractions.SetMovement(false);
        ConversationManager.Instance.StartConversation(thisConversation);
    }

    public void EndConversation()
    {
        ConversationManager.Instance.EndConversation();
        // Enable movement when conversation ends
        playerInteractions.SetMovement(true);
    }

    public void AddObjective(string objective)
    {
        CustomEvent.Trigger(this.gameManager, "AddObjective", objective);
    }

    public void CompleteObjective(string objective)
    {
        CustomEvent.Trigger(this.gameManager, "CompleteObjective", objective);
    }
}
