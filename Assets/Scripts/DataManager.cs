using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] List<WordScriptableObject> Animals;
    [SerializeField] List<WordScriptableObject> Objects;
    [SerializeField] List<WordScriptableObject> Countries;

    public List<string> Alphabets = new() {"a","b","c","d","e","f","g","h","i","j","k","l","m","n", "o","p", "q","r","s","t","u","v","w","x","y","z" };
    public int curAlphabet;
    public bool CompareGuess(Types guessType, string guess)
    {
        Debug.LogError("type: " + guessType + " guess: " + guess);
        switch (guessType)
        {
            case Types.animal:
                if(Animals[curAlphabet]._words.Contains(guess))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Types.objects:
                if(Objects[curAlphabet]._words.Contains(guess))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case Types.country:
                if(Countries[curAlphabet]._words.Contains(guess)) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
    public string GetAlphabet(int index)
    {
        return Alphabets[index].ToUpper();
    }
}
