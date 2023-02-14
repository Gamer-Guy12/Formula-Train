using UnityEngine;
using Unity.Netcode;

public class FinishPoint : NetworkBehaviour
{

    public GameObject winScreen;

    void OnCollisionEnter(Collision collision)
    {

        SpawnWinScreenServerRpc();
        WinScreenManager.Singleton.ShowWinScreen(collision.gameObject.GetComponent<Move>().OwnerClientId);

    }

    [ServerRpc]
    void SpawnWinScreenServerRpc()
    {

        GameObject winScreenSpawned = Instantiate(winScreen);
        winScreenSpawned.GetComponent<NetworkObject>().Spawn(true);

    }

}
