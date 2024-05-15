using System.Collections;
using DialogueEditor;
using StarterAssets;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private ConversationsManager conversationManager;
    [SerializeField] private Transform playerHeadTarget;
    [SerializeField] private Rig headRig;
    [SerializeField] private ThirdPersonController thirdPersonController;
    [SerializeField] private GameManagerScript gameManager;

    public Vector3 playerHeadHeight;

    private StarterAssetsInputs _input;
    private bool _isInteracting = false;

    private bool _isSitting = false;

    private Animator _animator;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (_isSitting)
        {
            thirdPersonController.SetMovement(false);
            conversationManager.ShowInteractionText("Press 'E' to stand up");
            if (_input.interact)
            {
                StandUp();
                conversationManager.HideInteractionText();
                _input.interact = false;
            }
        }
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.CompareTag("NPC"))
        {
            conversationManager.ShowInteractionText($"Press 'E' to interact");
            _isInteracting = false;
        }

        if (otherObject.CompareTag("Food"))
        {
            conversationManager.ShowInteractionText($"Press 'E' to pick up");
        }

        if (otherObject.CompareTag("Bench"))
        {
            conversationManager.ShowInteractionText($"Press 'E' to sit down");
        }
    }

    private void OnTriggerStay(Collider otherObject)
    {
        if (otherObject.CompareTag("NPC") && !_isInteracting && _input.interact)
        {
            try
            {
                _isInteracting = true;  // Set interacting flag to true
                NPCManager npcManager = otherObject.GetComponent<NPCManager>();

                // Make NPC look at the player
                LookAtTarget(npcManager);

                conversationManager.StartConversation(npcManager.getNPCConversation());
                conversationManager.HideInteractionText();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error: " + e.Message);
            }
        }

        if (otherObject.CompareTag("Food") && _input.interact)
        {
            conversationManager.HideInteractionText();
            Destroy(otherObject.gameObject);
            StartCoroutine(DisplayMessageCoroutine("1 Food item added to inventory", 2));
            gameManager.AddItemToInventory(otherObject.name);
            _input.interact = false;
        }

        if (otherObject.CompareTag("Bench") && _input.interact)
        {
            try
            {
                conversationManager.HideInteractionText();
                SitOnBench(otherObject.gameObject);
                _input.interact = false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    private IEnumerator DisplayMessageCoroutine(string message, float delay)
    {
        conversationManager.ShowInteractionText(message); // Show the message
        yield return new WaitForSeconds(delay); // Wait for 'delay' seconds
        conversationManager.HideInteractionText(); // Hide the message
    }

    private void OnTriggerExit(Collider otherObject)
    {
        if (otherObject.CompareTag("NPC"))
        {
            ResetTargetPosition(); // Resets the player's target
            NPCManager npcManager = otherObject.GetComponent<NPCManager>();
            npcManager.ResetTargetPosition(); // Resets the NPC's target

            conversationManager.HideInteractionText();
            conversationManager.EndConversation();
            _input.interact = false;
            _isInteracting = false;
        }

        conversationManager.HideInteractionText();
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

        _animator.Play("Idle Walk Run Blend");
    }

    public void LookAtTarget(NPCManager npcManager)
    {
        transform.LookAt(npcManager.transform);

        // Make NPC look at the player
        npcManager.SetTargetPosition(transform.position + playerHeadHeight);
        // Set the player head target to the NPC's head position
        playerHeadTarget.position = npcManager.transform.position + npcManager.npcHeadHeight;

        // Enable head rig
        headRig.weight = 1;
    }

    public void ResetTargetPosition()
    {
        headRig.weight = 0;
    }

    private void SitOnBench(GameObject bench)
    {
        // Ensure there is a bench and it has a predefined sit position as a child object named "SitPosition"
        Transform sitPosition = bench.transform.Find("SitPosition");
        if (sitPosition != null && !_isSitting)
        {
            bench.GetComponent<MeshCollider>().enabled = false; // Disable the player's collider
            // Move and rotate the player to the bench's sit position
            gameObject.transform.SetPositionAndRotation(sitPosition.position, sitPosition.rotation);

            // Trigger the sit animation
            TriggerAnimation("Sit");
            _isSitting = true;
        }
    }

    public void StandUp()
    {
        _isSitting = false;
        _animator.SetBool("Sitting", false);
        thirdPersonController.SetMovement(true);
    }
}
