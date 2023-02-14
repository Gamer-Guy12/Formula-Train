using TMPro;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{

    public static WinScreenManager Singleton;
    public TMP_Text winPlayer;
    public Canvas winScreen;

    void Awake()
    {

        Singleton = this;
        winScreen.gameObject.SetActive(false);

    }

    public void ShowWinScreen(ulong id)
    {

        winPlayer.text = "Player " + id.ToString() + " has won!!!";
        winScreen.gameObject.SetActive(true);

    }

}
