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

    [ServerRpc]
    public void AddWinScreenServerRpc(ulong id)
    {

        GameObject spawned = Instantiate(winScreen);
        spawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";
        spawned.GetComponent<NetworkObject>().Spawn(true);

    }

}
