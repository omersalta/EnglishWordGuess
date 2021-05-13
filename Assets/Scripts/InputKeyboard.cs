using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputKeyboard : MonoBehaviour {
    
    private Guesster Guesster;
    
    private char pressedKey;
    private List<GameObject> allKeys;

    private void Start() {
        allKeys = new List<GameObject>();
        foreach (Transform child in transform) {
            foreach (Transform key in child) {
                allKeys.Add(key.gameObject);
            }
        }
    }
    
    public void OnPressKey(GameObject key) {
        if (pressedKey == '\0') {
            pressedKey = key.name[0];
            key.GetComponent<Button>().interactable = false;
        }
    }
    
    public char GetPressedKey() {
        var curretKey = pressedKey;
        pressedKey = '\0';
        return curretKey;
    }
    
    public void ResetMyKeyboard() {
        foreach (var key in allKeys) {
            if (!key.GetComponent<Button>().interactable) {
                key.GetComponent<Button>().interactable = true;
            }
        }
    }
    
    public void LockMyKeyboard() {
        foreach (var key in allKeys) {
            key.GetComponent<Button>().interactable = false;
        }
    }

    public bool isPressedAnyKey() {
        if (pressedKey == '\0') {
            return false;
        }
        return true;
    }

    public void BackToMainMenuButton() {
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("click");
    }
}
