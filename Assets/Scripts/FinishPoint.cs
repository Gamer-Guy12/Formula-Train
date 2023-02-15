using UnityEngine;
using Unity.Netcode;

public class FinishPoint : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {

        WinScreenManager.Singleton.AddWinScreenServerRpc(collision.gameObject.GetComponent<Move>().OwnerClientId);

    }

}
