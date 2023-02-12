using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField]private UnityTransport transport;

    [SerializeField]private Camera UICam;
    [SerializeField]private Canvas joinCanvas;

    [SerializeField]private Button host;
    [SerializeField]private Button join;
    [SerializeField]private TMP_InputField IPinput;

    void Awake()
    {

        host.onClick.AddListener(() => 
        {

            transport.ConnectionData.Address = IPinput.text;
            NetworkManager.Singleton.StartHost();
            UICam.gameObject.SetActive(false);
            joinCanvas.gameObject.SetActive(false);
        
        });

        join.onClick.AddListener(() =>
        {

            transport.ConnectionData.Address = IPinput.text;
            NetworkManager.Singleton.StartClient();
            UICam.gameObject.SetActive(false);
            joinCanvas.gameObject.SetActive(false);

        });

    }

}
