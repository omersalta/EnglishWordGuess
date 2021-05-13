using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    
    private Object[] objects;
    private List<GameObject> ZoneObjects;
    private State currentState;
    private Guesster Guesster;
    private InputKeyboard IK;
    public TextMeshProUGUI lifeTxt;
    public TextMeshProUGUI gameOverTxt;
    public GameObject gameOverPannel;
    enum State {
        INITIAL,
        ASKING,
        GUESSING,
        NEXT_WORD,
        GAME_OVER
    };
    
    private void Start() {
        ZoneObjects = new List<GameObject>();
        Guesster = FindObjectOfType<Guesster>();
        currentState = State.INITIAL;
        IK = FindObjectOfType<InputKeyboard>();
    }
    
    void Update() {
        switch (currentState) {
            case State.INITIAL:
                //make sure game ready to start go asking state
                IK.gameObject.SetActive(true);
                gameOverPannel.SetActive(false);
                PLAYER.StartPlayer(PLAYER.getSelectedDifficulty());
                ZoneObjects = CreateObjectList(PLAYER.getSelectedZone());
                UpdateRemainLife();
                Invoke("PlayZoneMusic",0.2f);
                currentState = State.ASKING;
                break;
            case State.ASKING:
                //get object from pool and call newGuess function and go guessing state
                Invoke("pushNewGuess", 0.8f);
                currentState = State.GUESSING;
                break;
            case State.GUESSING:
                if (IK.isPressedAnyKey()) {
                    Guesster.SearchLetter(IK.GetPressedKey());
                    UpdateRemainLife();
                }
                if (Guesster.succesfulGuess) {
                    Guesster.succesfulGuess = false;
                    //TODO check for is it working
                    ZoneObjects.Remove(Guesster.GetLastGivenGameObject());
                    currentState = State.NEXT_WORD;
                    break;
                }
                
                //check life count and if die go game over pannel if guess succes go next word state
                if (PLAYER.AmIDead()) {
                    currentState = State.GAME_OVER;
                }
               
                break;
            case State.NEXT_WORD:
                //make sure is there object in pool and if it go ask if go game over state
                if (ZoneObjects.Any()) {
                    currentState = State.ASKING;
                }else {
                    currentState = State.GAME_OVER;
                }
                break;
            case State.GAME_OVER:
                IK.gameObject.SetActive(false);
                gameOverPannel.SetActive(true);
                if (PLAYER.GetRemainingLife() > 0) {
                    gameOverTxt.text = "YOU WIN \n total word guessed : " +
                                       PLAYER.GetSuccesfulGueesCount() +
                                       " \n wrong gues :" + PLAYER.GetWrongGuessCount();
                }else {
                    gameOverTxt.text = "YOU LOSE \n total word guessed : " +
                                       PLAYER.GetSuccesfulGueesCount() +
                                       " \n wrong gues :" + PLAYER.GetWrongGuessCount();
                }
                break;
        }
    }


    void PlayZoneMusic() {
        FindObjectOfType<AudioManager>().Play(PLAYER.getSelectedZone());
    }
    void UpdateRemainLife() {
        lifeTxt.text = PLAYER.GetRemainingLife().ToString();
    }

        void pushNewGuess() {
        //we sure list contains any object
        Guesster.NewGuessObject(ZoneObjects[Random.Range(0,ZoneObjects.Count)]);
        IK.ResetMyKeyboard();
    }
    
    List<GameObject> CreateObjectList(string path) {
        List<GameObject> tempList = new List<GameObject>();
        foreach (var go in Resources.LoadAll<GameObject>(path))
        {
            tempList.Add(go);
        }
        return tempList;
    }

}
