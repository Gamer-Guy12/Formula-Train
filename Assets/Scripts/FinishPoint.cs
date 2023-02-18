using UnityEngine;
using Unity.Netcode;

public class FinishPoint : NetworkBehaviour
{

    public WinScreenManager manager;
    public NetworkManager netManager;

    void OnCollisionEnter(Collision collision)
    {

        manager.AddWinScreenServerRpc(collision.gameObject.GetComponent<Move>().OwnerClientId);
        manager.AddWinScreen(collision.gameObject.GetComponent<Move>().OwnerClientId);

        netManager.gameObject.SetActive(false);

    }

}
