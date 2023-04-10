using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject characterObject;
    [SerializeField] private string characterName;
    public void Initialize()
    {
        // transform.position = new Vector3(UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_x"),
        // UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_y"),
        // UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_z"));
    }
    public void Reveal()
    {
        characterObject.SetActive(true);
        animator.Play("Base Layer.main", 0, 0.25f);
    }

    public void Hide()
    {
        characterObject.SetActive(false);
        animator.StopPlayback();
    }
}
