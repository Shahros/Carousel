using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Animator _machineHandle, _startScreen;
    [SerializeField] Button _submitBtn;
    [SerializeField] Image _curGuessableImage;
    [SerializeField] InputField _inputField;
    [SerializeField] Text _finalText;
    [SerializeField] GameObject _seperator, _guessArea, _gameEndScreen, _confetti;

    [SerializeField] Image[] _toGuessImages;
    [SerializeField] Sprite[] _guessableSprites;
    [SerializeField] GameObject[] _correctImages;
    [SerializeField] Animator[] _slotsAnimators;

    [SerializeField] List<Image> slotImages2, slotImages3, slotImages4;
    [SerializeField] List<TMP_Text> slot1Texts;
    readonly WaitForSeconds waitTime = new(0.25f);
    int startingSlotIndex = 0;
    public void SpinMachine()
    {
        if (GameManager.Instance.isGameStarted)
            return;
        GameManager.Instance.isGameStarted = true;
        StartCoroutine(PlaySlotAnimation());
        
    }
    IEnumerator PlaySlotAnimation()
    {
        _machineHandle.Play("Handle_Play");
        yield return new WaitForSeconds(0.5f);
        foreach (Animator slot in _slotsAnimators)
        {
            slot.Play("SlotSpin");
            yield return waitTime;
        }
        yield return new WaitForSeconds(1.5f);
        foreach (Image item in _toGuessImages)
        {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        yield return waitTime;
        UpdateObjectToGuess(startingSlotIndex);
        yield return new WaitForSeconds(2f);
        ResetSlots();
    }
    void ResetSlots()
    {
        slot1Texts[9].text = slot1Texts[0].text;
        slotImages2[9].sprite = slotImages2[0].sprite;
        slotImages3[9].sprite = slotImages2[0].sprite;
        slotImages4[9].sprite = slotImages2[0].sprite;

        foreach (Animator slot in _slotsAnimators)
        {
            slot.GetComponent<RectTransform>().localPosition = new Vector3(0, 1485f, 0);
        }
    }
    public void SetStartingSlots(string _letter,int slot2, int slot3, int slot4, string _randomLetter)
    {
        startingSlotIndex = slot2;
        slot1Texts[0].text = _letter;
        slotImages2[0].sprite = _guessableSprites[slot2];
        slotImages3[0].sprite = _guessableSprites[slot3];
        slotImages4[0].sprite = _guessableSprites[slot4];
        for(int i=1; i<9; i++)
        {
            slotImages2[i].sprite = _guessableSprites[Random.Range(0, 3)];
            slotImages3[i].sprite = _guessableSprites[Random.Range(0, 3)];
            slotImages4[i].sprite = _guessableSprites[Random.Range(0, 3)];
        }
        if(!GameManager.Instance.isGameStarted)
        {
            slot1Texts[9].text = _randomLetter;
            slotImages2[9].sprite = _guessableSprites[Random.Range(0, 3)];
            slotImages3[9].sprite = _guessableSprites[Random.Range(0, 3)];
            slotImages4[9].sprite = _guessableSprites[Random.Range(0, 3)];
        }
        SetupToGuessImages(slot2, slot3, slot4);
    }
    public void SetupToGuessImages(int slot2, int slot3, int slot4)
    {
        _toGuessImages[0].sprite = _guessableSprites[slot2]; 
        _toGuessImages[1].sprite = _guessableSprites[slot3];
        _toGuessImages[2].sprite = _guessableSprites[slot4];
    }
    public void UpdateObjectToGuess(int index)
    {
        Debug.Log("cur index: " + index);
        _inputField.interactable = true;
        _curGuessableImage.gameObject.SetActive(false);
        _curGuessableImage.sprite = _guessableSprites[index];
        _curGuessableImage.gameObject.SetActive(true);
    }
    public void UpdateInputText(string s)
    {
        GameManager.Instance.curGuess = s.ToLower();
        if(s == "" || s == null)
        {
            _submitBtn.interactable = false;
        }
        else
        {
            _submitBtn.interactable = true;
        }
    }
    public void ChangeStateOfIndicator(bool state, int index)
    {
        Debug.LogError("completed: " + index);
        _correctImages[index].SetActive(state);
        _inputField.text = "";
    }
    public void StartGame()
    {
        StartCoroutine(StartingRoutine());
    }
    IEnumerator StartingRoutine()
    {
        _startScreen.Play("StartGame");
        yield return new WaitForSeconds(1f);
        _seperator.SetActive(true);
        _guessArea.SetActive(true);
    }
    public void GameEnd(bool didWin, string _result)
    {
        if (didWin)
            _confetti.SetActive(true);
        _finalText.text = _result;
        _gameEndScreen.SetActive(true);
    }
}
