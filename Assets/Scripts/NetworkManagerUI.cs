using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField]private Camera UICam;
    [SerializeField]private Canvas joinCanvas;

    [SerializeField]private Button host;
    [SerializeField]private Button join;

    void Awake()
    {

        host.onClick.AddListener(() => 
        {

            NetworkManager.Singleton.StartHost();
            UICam.gameObject.SetActive(false);
            joinCanvas.gameObject.SetActive(false);
        
        });

        join.onClick.AddListener(() =>
        {

            NetworkManager.Singleton.StartClient();
            UICam.gameObject.SetActive(false);
            joinCanvas.gameObject.SetActive(false);

        });

    }

}
