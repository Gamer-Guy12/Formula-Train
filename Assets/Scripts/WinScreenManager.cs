using Unity.Netcode;
using UnityEngine;
using TMPro;

public class WinScreenManager : MonoBehaviour
{

    public static WinScreenManager Singleton;
    public GameObject winScreen;

    void Awake()
    {

        Singleton = this;

    }

    public void ShowWinScreen(ulong id)
    {

        GameObject spawned = Instantiate(winScreen);
        spawned.GetComponent<NetworkObject>().Spawn(true);
        spawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";

    }

}
