using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  UIManager: MonoBehaviour
{

    public void Cancel()
    {


    }

    public void Quit()
    {
       Application.Quit();
    }

    public void Level(int index)
    {
        SceneManager.LoadScene(index);
    }

}
