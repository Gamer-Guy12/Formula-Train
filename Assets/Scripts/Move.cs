using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using TMPro;

public class Move : NetworkBehaviour
{

    public List<Vector3> spawnPoints = new List<Vector3>();

    public Camera cam;
    public NetworkVariable<bool> winnerExists = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    

    [Header("Move")]
    public float moveSpeed;

    [Header("Turn")]
    public float turnSpeed;

    [Header("Keys")]
    public KeyCode moveKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.D;
    public KeyCode rightKey = KeyCode.A;
    public KeyCode brakeKey = KeyCode.S;

    [Header("Refrences")]
    public Rigidbody rb;
    public TMP_Text id;
    public GameObject idCanvas;

    public override void OnNetworkSpawn()
    {
        
        winnerExists.OnValueChanged += (bool previous, bool current) => {

            rb.constraints = RigidbodyConstraints.FreezePosition;

            winnerExists = new NetworkVariable<bool>(current, NetworkVariableReadPermission.Everyone);

        };

        if (IsLocalPlayer)
        {

            cam.enabled = true;

            idCanvas.SetActive(true);
            id.text = OwnerClientId.ToString();

            transform.position = spawnPoints[(int)OwnerClientId];

        }

        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {

        rb.AddForce(Vector3.down * 100f, ForceMode.Acceleration);

        if (Physics.Raycast(transform.position, transform.up, 3f)) transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
        if (Physics.Raycast(transform.position, -transform.up, (float)1.813221 / 2f + 0.001f)) transform.rotation = Quaternion.Euler(0f, transform.rotation.y, transform.rotation.z);

        if (!IsOwner) return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }

        if (Input.GetKey(moveKey))
        {

            rb.AddForce(transform.right * moveSpeed * 10, ForceMode.Force);

        }

        if (Input.GetKey(leftKey))
        {
            transform.RotateAround(transform.position, Vector3.up, turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(rightKey))
            transform.RotateAround(transform.position, -Vector3.up, turnSpeed * Time.deltaTime);

        if (Input.GetKey(brakeKey))
        {

            if (rb.velocity.magnitude > 0)
            {

                rb.velocity = Vector3.zero;

            }
            rb.AddForce(-transform.right * moveSpeed * 5, ForceMode.Force);

        }
   
    }

}
