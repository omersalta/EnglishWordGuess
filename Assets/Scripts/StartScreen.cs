using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScreen : MonoBehaviour {
    
    
    public List<Image> difList;
    public List<Image> zoneList;

    private void Start() {
        FindObjectOfType<AudioManager>().Play("startMusic");
    }

    public void StartButton() {
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("start button preessed");
        SceneManager.LoadScene(1);
    }
    
    public void Selection(GameObject button) {
        FindObjectOfType<AudioManager>().Play("pressKey");
        PLAYER.setSelectedZone(button.name);
        foreach (var item in zoneList) {
            item.color = new Color32(39,77,123,80);
        }
        button.GetComponent<Image>().color = new Color32(0,0,0,90);
    }
    
    public void OnDifficultyCahange(GameObject button) {
        FindObjectOfType<AudioManager>().Play("pressKey");
        PLAYER.setSelectedDifficulty(button.name);
        foreach (var item in difList) {
            item.color = new Color32(39,77,123,80);
        }
        button.GetComponent<Image>().color = new Color32(0,0,0,90);
    }

    public void Quit() {
        FindObjectOfType<AudioManager>().StopAll();
        Application.Quit();
    }

}
