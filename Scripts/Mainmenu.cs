using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void LoadGame()//this method is called when we hit the new game button
    {
        SceneManager.LoadScene(1);//loads scene(1)=game scene
    }

    public void QuitGame()//this method is called when we hit the quit button
    {
        Application.Quit();//quits the application
    }
}