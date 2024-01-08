using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{



    public void PlayGame()
    {
        SceneManager.LoadScene(1); //Load Scene when u press the button - event

    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit(); //Quit the application when u press the button - event
    }



}
