using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables; // If you're using Timeline

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector if using Timeline

    [SerializeField] private GameObject ZoroObject;
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private ConversationsManager _conversationsManager;
    [SerializeField] private GameObject GameManager;

    public void PlayCutscene()
    {
        if (playableDirector != null)
        {
            playerInteractions.SetMovement(false);
            playableDirector.Play();
            Invoke(nameof(EndCutscene), 40);
        }
        else
        {
            Debug.LogError("PlayableDirector not assigned");
        }
    }

    private void EndCutscene()
    {
        Destroy(ZoroObject);
        gameObject.SetActive(false);
        _conversationsManager.EndConversation();
        _conversationsManager.CompleteObjective("Kill Zoro!");
        _conversationsManager.ShowInteractionText("Level Completed!");
        CustomEvent.Trigger(GameManager, "IncreaseMoney", 1000);
        GameManager.GetComponent<GameManagerScript>().AddItemToInventory("Axe");
        playerInteractions.SetMovement(true);
    }
}