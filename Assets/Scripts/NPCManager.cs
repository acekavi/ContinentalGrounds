using UnityEngine;
using DialogueEditor;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    private List<NPCConversation> Dialogs;
    public Vector3 npcHeadHeight;

    private Animator _animator;

    private int _currentDialogIndex = 0;

    [SerializeField] private Transform npcHeadTarget;
    public Rig headRig;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public NPCConversation getNPCConversation()
    {
        return Dialogs[_currentDialogIndex];
    }

    public void TriggerAnimation(string animationName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // If the animation is currently running, reset the trigger
            _animator.ResetTrigger(animationName);
        }
        else
        {
            // If not, set the trigger to start the animation
            _animator.SetTrigger(animationName);
        }
    }

    public void StopAnimation(string animationName)
    {
        // Reset the trigger to ensure the animation stops if it was triggered
        _animator.ResetTrigger(animationName);

        // Optionally, you may want to force the animator back to a default state
        _animator.Play("IdleState"); // Replace "IdleStateName" with your actual idle state
    }

    public void NextDialog()
    {
        _currentDialogIndex++;
        if (_currentDialogIndex >= Dialogs.Count)
        {
            _currentDialogIndex = 0;
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        npcHeadTarget.position = targetPosition;
        headRig.weight = 1;
    }

    public void ResetTargetPosition()
    {
        headRig.weight = 0;
    }
}
