using Unity.Netcode;
using UnityEngine;
using TMPro;

public class WinScreenManager : NetworkBehaviour
{

    public GameObject winScreen;
    private GameObject spawned;
    private GameObject privateSpawned;
    public NetworkManager netManager;

    bool hasWinner = false;

    public GameObject startCanvas;
    public GameObject barrier;

    public void StartGame()
    {

        startCanvas.SetActive(false);
        barrier.SetActive(false);

    }

    public override void OnNetworkSpawn()
    {

        startCanvas.SetActive(false);

        if (IsHost)
        {

            startCanvas.SetActive(true);
            barrier.SetActive(true);

        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void AddWinScreenServerRpc(ulong id, ServerRpcParams rpc = default)
    {

        if (hasWinner) return;

        spawned = Instantiate(winScreen);
        spawned.GetComponentInChildren<TMP_Text>().text = "Player " + id.ToString() + " Has Won";
        spawned.GetComponent<NetworkObject>().Spawn(true);

        hasWinner = true;

        netManager.gameObject.SetActive(false);

    }

}
