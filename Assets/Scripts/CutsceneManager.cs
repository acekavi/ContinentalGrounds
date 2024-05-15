using UnityEngine;
using UnityEngine.Playables; // If you're using Timeline

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector if using Timeline

    [SerializeField] private GameObject ZoroObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private ConversationsManager _conversationsManager;

    public void PlayCutscene()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
            _animator.SetFloat("Speed", 0);
            Invoke(nameof(EndCutscene), 40);
        }
        else
        {
            Debug.LogError("PlayableDirector not assigned");
        }
    }

    private void EndCutscene()
    {
        ZoroObject.SetActive(false);
        gameObject.SetActive(false);
        _animator.Play("Idle Walk Run Blend");
        _conversationsManager.EndConversation();
        _conversationsManager.CompleteObjective("Kill Zoro!");
        _conversationsManager.ShowInteractionText("Level Completed!");
    }
}