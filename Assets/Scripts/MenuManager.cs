using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    

    public void BeginGame(string difficulty)
    {
        SystemManager.Instance.difficulty = difficulty;
        SceneManager.LoadScene("MainScene");
    }
}
