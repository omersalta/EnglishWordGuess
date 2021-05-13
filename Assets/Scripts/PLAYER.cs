using System;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour{

    #region Singleton Instance region
    private static PLAYER instance = null;
    private void Awake() {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }

        instance = this;
        //DontDestroyOnLoad( this.gameObject );
    }

    // Game Instance Singleton
    public static PLAYER Instance {
        get { return instance; }
    }
    #endregion

    private static int wronGuess;
    private static int lifeCount;
    private static int succesfulGueesCount;
    private static List<int> DifficultiesList = new List<int>() {10,5,3,1};
    private static Zones selectedZone;
    private static Difficulties selectedDifficulty;
    
    public enum Zones 
    {
        School,
        Camp,
    }
    
    public enum Difficulties
    {
        EASY,
        MEDIUM,
        HARD,
        VERYHARD,
    }

    public static void setSelectedZone(string selected) {
        int i = 0;
        string[] ZoneNames = Enum.GetNames(typeof(Zones));
        foreach (var zoneName in ZoneNames) {
            if (zoneName == selected) {
                selectedZone = (Zones) i;
            }
            i++;
        }
    }

    public static string  getSelectedZone() {
        return selectedZone.ToString();
    }
    
    public static void setSelectedDifficulty(string selected) {
        int i = 0;
        string[] difficultyNames = Enum.GetNames(typeof(Difficulties));
        foreach (var difficut in difficultyNames) {
            if (difficut == selected) {
                selectedDifficulty = (Difficulties) i;
            }
            i++;
        }
        Debug.Log((int)selectedDifficulty);
        Debug.Log("diff guess count: " +getSelectedDifficulty());
    }
    
    public static int getSelectedDifficulty() {
        
        return DifficultiesList[(int)selectedDifficulty];
    }


    public static void StartPlayer(int guessesCount) {
        lifeCount = guessesCount;
        wronGuess = 0;
        succesfulGueesCount = 0;
    }

    public static void WrongGuess() {
        FindObjectOfType<AudioManager>().Play("wrong");
        lifeCount--;
        wronGuess++;
    }
    
    public static void SuccesfulGuees() {
        FindObjectOfType<AudioManager>().Play("correct");
        succesfulGueesCount++;
    }
    
    
    public static bool AmIDead() {
        if (lifeCount > 0)
            return false;
        return true;
    }

    public static int GetRemainingLife() {
        return lifeCount;
    }
    
    public static int GetSuccesfulGueesCount() {
        return succesfulGueesCount;
    }
    
    public static int GetWrongGuessCount() {
        return wronGuess;
    }
    
    
    

}
