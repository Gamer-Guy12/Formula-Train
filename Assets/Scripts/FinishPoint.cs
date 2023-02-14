using UnityEngine;
using Unity.Netcode;

public class FinishPoint : MonoBehaviour
{

    public GameObject winScreen;

    void OnCollisionEnter(Collision collision)
    {

        WinScreenManager.Singleton.ShowWinScreen(collision.gameObject.GetComponent<Move>().OwnerClientId);

    }

}
