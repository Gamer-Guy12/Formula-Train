using Unity.Netcode;
using UnityEngine;
using TMPro;

public class WinScreenManager : NetworkBehaviour
{

    public GameObject winScreen;
    private GameObject spawned;
    private GameObject privateSpawned;

    public void AddWinScreen(ulong id)
    {

        spawned.SetActive(false);
        privateSpawned = Instantiate(winScreen);
        privateSpawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";

    }

    [ServerRpc(RequireOwnership = false)]
    public void AddWinScreenServerRpc(ulong id, ServerRpcParams rpc = default)
    {

        spawned = Instantiate(winScreen);
        spawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";
        spawned.GetComponent<NetworkObject>().Spawn(true);

    }

}
