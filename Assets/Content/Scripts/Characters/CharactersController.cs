using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharactersController : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    static UnityEvent onCharacterChanged;
    private static Character currentCharacter;
    public static Character CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }
        set
        {
            if (value != currentCharacter)
            {
                currentCharacter = value;
                onCharacterChanged.Invoke();
                CanChangeCharacter = false;
            }
        }
    }

    public static bool CanChangeCharacter;
    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        onCharacterChanged = new UnityEvent();
        onCharacterChanged.AddListener(() => setCharacter());
        CanChangeCharacter = true;
    }

    void setCharacter()
    {
        foreach (Character character in characters)
        {
            character.Hide();
        }
        currentCharacter.Reveal();
    }
}
