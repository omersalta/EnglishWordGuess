using UnityEngine;
using System.Text;
using System.Globalization;

public class Guesster : MonoBehaviour {

    public static bool succesfulGuess;
    public Transform spawnPoint;
    
    
    private GameObject lastGivenAddress;
    private TextMesh GuessTextMesh;
    private GameObject currentObject;
    private StringBuilder nameTryingGuess;   
    private StringBuilder guessingString;
    
    void Start() {
        succesfulGuess = false;
        GuessTextMesh = GetComponentInChildren<TextMesh>();
        //GuessTextMesh.transform.position = new Vector3(100,-4.7f,1.18f);
        guessingString = new StringBuilder();
        nameTryingGuess = new StringBuilder();
        //SearchLetter('A');
    }

    public void NewGuessObject(GameObject go) {
        lastGivenAddress = go.gameObject;
        Destroy(currentObject);
        currentObject = Instantiate(go);
        currentObject.transform.position = spawnPoint.position;
        AddAnimation(currentObject);
        nameTryingGuess.Clear();
        var preparedString = currentObject.name.ToUpper(new CultureInfo("en-US", false));
        preparedString = preparedString.Replace("(CLONE)", "");
        foreach (char c in preparedString) {
            nameTryingGuess.Append(c+" ");
        }
        nameTryingGuess.Remove(nameTryingGuess.Length-1, 1);
        ResetGuessString();
    }
    
    void ResetGuessString() {
        guessingString.Clear();
        foreach (char c in nameTryingGuess.ToString()) {
            if (c != ' ') {
                guessingString.Append('_');
            }else {
                guessingString.Append(' ');
            }
        }
        GuessTextMesh.text = guessingString.ToString();
    }
    
    
    

    public void SearchLetter(char searchingChar) {
        Debug.Log("searching char is : " + searchingChar);
        bool GuessFinded = true;
        bool charfinded = false;
        for (int i = 0; i < nameTryingGuess.Length; i++) {
            if (nameTryingGuess[i] == searchingChar) {
                guessingString[i] = searchingChar;
                charfinded = true;
            }
            if (guessingString[i] == '_') {
                GuessFinded = false;
            }
        }
        
        if (charfinded) {
            //call gussed succes func.
            if (!GuessFinded)
                FindObjectOfType<AudioManager>().Play("pressKey");
            GuessTextMesh.text = guessingString.ToString();
        }else {
            PLAYER.WrongGuess();
        }

        if (GuessFinded) {
            PLAYER.SuccesfulGuees();
            FindObjectOfType<InputKeyboard>().LockMyKeyboard();
            succesfulGuess = true;
        }
        
        Debug.Log("guessingString : " + guessingString );
        Debug.Log("nameTryingGuess : " + nameTryingGuess );
    }

    
    void AddAnimation(GameObject go) {
        go.AddComponent<RotateRight>();
    }

    public GameObject GetLastGivenGameObject() {
        return lastGivenAddress;
    }
    
}
