using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject characterObject;
    [SerializeField] private string characterName;
    public UnityEvent<Character> OnAnimationCompleted;
    public bool IsMainCharacter;
    private float startNorth;
    private float colliderScale, animatedCharacterScale;
    // public bool testTransform;
    // [SerializeField] private float testYangle, testDistance, testYoffset;

    // private void Update()
    // {
    //     if (testTransform)
    //     {
    //         var testPos = Camera.main.transform.position + (Quaternion.Euler(0, -startNorth + testYangle, 0) * Vector3.forward * testDistance);
    //         testPos.y = testYoffset;
    //         transform.position = testPos;
    //     }
    // }
    public void Initialize()
    {
        Debug.Log($"Character {characterName} is initializing");
        var basePos = Camera.main.transform.position;
        basePos += GetNorthDirection(UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_yOffset"),
                        UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_yAngle"),
                        UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_dist"));
        colliderScale = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_colScale");
        animatedCharacterScale = UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_charScale");
        // transform.localPosition = new Vector3(UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_x"),
        // UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_y"),
        // UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_z"));
        transform.position = basePos;
        transform.localScale = new Vector3(colliderScale, colliderScale, colliderScale);
        characterObject.transform.localScale = new Vector3(animatedCharacterScale, animatedCharacterScale, animatedCharacterScale);
        IsMainCharacter = UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), $"{characterName}_isMain") == "true" ? true : false;
        OnAnimationCompleted = new UnityEvent<Character>();
    }

    public Vector3 GetNorthDirection(float yOffset, float yAngle, float dist)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            float distance1 = dist;
            startNorth = 0f;
            startNorth = Input.location.status == LocationServiceStatus.Running ? Input.compass.magneticHeading : 0f;
            Quaternion rotation1 = Quaternion.Euler(0, -startNorth + yAngle, 0);
            var vec1 = rotation1 * Vector3.forward * distance1;
            vec1.y = yOffset;
            Debug.Log(vec1);
            return vec1;
        }
        else
        {
            float distance = dist;
            startNorth = Input.location.status == LocationServiceStatus.Running ? Input.compass.magneticHeading : 0f;
            Quaternion rotation = Quaternion.Euler(0, -startNorth + yAngle, 0);
            var vec = rotation * Vector3.forward * distance;
            vec.y = yOffset;
            Debug.Log(vec);
            return vec;
        }
    }

    public void Reveal()
    {
        if (!IsMainCharacter && animator.GetBool("isAnim")) return;
        Debug.Log($"{characterName} reveal");
        characterObject.SetActive(true);
        animator.SetBool("isAnim", true);
        StartCoroutine(OnCompleteAnimation());
    }
    IEnumerator OnCompleteAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
        Debug.Log($"{characterName} animation completed");
        Hide();
        OnAnimationCompleted.Invoke(this);
    }
    public void Hide()
    {
        Debug.Log($"{characterName} hide");
        characterObject.SetActive(false);
        animator.SetBool("isAnim", false);
    }
}
