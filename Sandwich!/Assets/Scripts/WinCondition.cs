using System;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static Action<int> Turns;
    [SerializeField] int moveToWin;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject GameUI;

    private void OnEnable()
    {
        Turns += Win;

    }
    private void OnDisable()
    {
        Turns += Win;
    }
    public void Win(int turns)
    {
        if(turns ==moveToWin)
        {
            GameUI.SetActive(false);
            WinScreen.SetActive(true);

        }
    }
}
