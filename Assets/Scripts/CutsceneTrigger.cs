using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutsceneObject;
    private CutsceneManager cutsceneManager;

    private void Start()
    {
        cutsceneManager = cutsceneObject.GetComponent<CutsceneManager>();
        if (cutsceneManager == null)
        {
            Debug.LogError("CutsceneManager not found on cutsceneObject");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutsceneManager.PlayCutscene();
        }
    }
}