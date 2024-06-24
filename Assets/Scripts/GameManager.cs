using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager uiManager;
    public DataManager dataManager;
    public string curGuess;
    public int curStep = 0;
    public bool isGameStarted = false;
    int[] _slots = new int[4];
    int curGuessableIndex = 0;
    List<string> alreadyGuessed = new();
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GenerateRandomSet();
    }
    void GenerateRandomSet()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        _slots[0] = Random.Range(0, 26);
        _slots[1] = Random.Range(0, 3);
        _slots[2] = Random.Range(0, 3);
        _slots[3] = Random.Range(0, 3);
        curGuessableIndex = _slots[1]; 
        dataManager.curAlphabet = _slots[0];
        string randomLetter = dataManager.GetAlphabet(Random.Range(0, 26));
        uiManager.SetStartingSlots(dataManager.GetAlphabet(_slots[0]), _slots[1], _slots[2], _slots[3], randomLetter);
    }
    public void OnAnswerSubmit()
    {
        if(alreadyGuessed.Count > 0)
        {
            if(alreadyGuessed.Contains(curGuess))
            {
                return;
            }
            else
            {
                alreadyGuessed.Add(curGuess);
            }
        }
        else
        {
            alreadyGuessed.Add(curGuess);
        }
       if(dataManager.CompareGuess((Types)curGuessableIndex, curGuess))
       {
            uiManager.ChangeStateOfIndicator(true, curStep);
            curStep++;
            if(curStep == 3)
            {
                Debug.Log("win...");
                uiManager.GameEnd(true, curStep.ToString());
                //win...
            }
            else
            {
                curGuessableIndex = _slots[curStep + 1];
                uiManager.UpdateObjectToGuess(curGuessableIndex);
                
            }

       }
        else
        {
            Debug.Log("Wrong Answer...");
            uiManager.GameEnd(false, curStep.ToString());
            //Game over
        }
    }
    public void ReplayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

public enum Types
{
    animal,
    objects,
    country
}
