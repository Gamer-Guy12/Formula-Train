using Unity.Netcode;
using UnityEngine;
using TMPro;

public class WinScreenManager : NetworkBehaviour
{

    public GameObject winScreen;

    [ServerRpc]
    public void AddWinScreenServerRpc(ulong id)
    {

        GameObject spawned = Instantiate(winScreen);
        spawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";
        spawned.GetComponent<NetworkObject>().Spawn(true);

    }

}
