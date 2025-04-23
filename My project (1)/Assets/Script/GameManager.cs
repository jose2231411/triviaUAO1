using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField nameInputField;
    private string playername;
    private string pendingSubject;
    private string subject;
    private int score;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Importante: desuscribirse
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gamesubject(string subject)
    {

        pendingSubject = subject;
        SceneManager.LoadScene("EscenaJuegoPreguntas");
    }
    public void sceneLoader(string scene) 
    {
        SceneManager.LoadScene(scene);
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (!string.IsNullOrEmpty(pendingSubject))
        {
            GameController gc = FindObjectOfType<GameController>();
            if (gc != null)
            {
                gc.currSubject = pendingSubject;
                Debug.Log("Asignado subject en nueva escena: " + pendingSubject);
            }
            else
            {
                Debug.LogWarning("GameController no encontrado en la nueva escena.");
            }

           
        }
    }
    public void getPlayername() 
    {
        string text = nameInputField.text;
        playername = text;
    }

    public void getScore() 
    {
        GameObject controller = GameObject.FindGameObjectWithTag("controller");
        GameController script = controller.GetComponent<GameController>();
        score = (int) script.score;
    }
    
    public void saveData() 
    {
        Save.SavePlayer(score, playername);
    }
}
