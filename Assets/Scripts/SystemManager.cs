using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public static SystemManager Instance { get; private set; } // Encapsulation

    private string difficulty_Actual; // Encapsulation
    public string difficulty { 
        get { return difficulty_Actual; } 
        set { if (value == "Easy" || value == "Normal" || value == "Hard") { difficulty_Actual = value; } } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BeginGame(string difficulty)
    {
        SystemManager.Instance.difficulty = difficulty;
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
