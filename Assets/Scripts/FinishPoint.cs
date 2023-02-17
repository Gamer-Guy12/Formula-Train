using UnityEngine;
using Unity.Netcode;

public class FinishPoint : NetworkBehaviour
{

    public WinScreenManager manager;

    void OnCollisionEnter(Collision collision)
    {

        manager.AddWinScreenServerRpc(collision.gameObject.GetComponent<Move>().OwnerClientId);
        manager.AddWinScreen(collision.gameObject.GetComponent<Move>().OwnerClientId);

    }

}
