using UnityEngine;
using UnityEngine.Events;

public class CharactersController : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    UnityEvent onMainCharacter;
    UnityEvent<Character> onAdditionalCharacter;
    private Character currentCharacter;
    public Character CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }
        set
        {
            if (!value.IsMainCharacter)
            {
                onAdditionalCharacter.Invoke(value);
            }
            else
            {
                if (value != currentCharacter && CanChangeCharacter)
                {
                    currentCharacter = value;
                    onMainCharacter.Invoke();
                    CanChangeCharacter = false;
                }
            }
        }
    }

    public bool CanChangeCharacter;

    public void Initialize()
    {
        Debug.Log("CharacterController init");
        onMainCharacter = new UnityEvent();
        onMainCharacter.AddListener(() => setMainCharacter());
        onAdditionalCharacter = new UnityEvent<Character>();
        onAdditionalCharacter.AddListener((s) => setAdditionalCharacter(s));
        foreach (Character character in characters)
        {
            character.Initialize();
            character.OnAnimationCompleted.AddListener((s) =>
            {
                Debug.Log(s.IsMainCharacter + " is main");
                if (s.IsMainCharacter)
                {
                    CanChangeCharacter = true;
                }
            });
        }
        CanChangeCharacter = true;
    }

    void setMainCharacter()
    {
        foreach (Character character in characters)
        {
            if (character.IsMainCharacter)
                character.Hide();
        }
        currentCharacter.Reveal();
    }

    void setAdditionalCharacter(Character character)
    {
        character.Reveal();
    }
}
