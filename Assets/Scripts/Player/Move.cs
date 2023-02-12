using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class Move : NetworkBehaviour
{

    public NetworkVariable<int> index = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>();
    public List<Vector3> indexer = new List<Vector3>();

    public Camera cam;

    [Header("Move")]
    public float moveSpeed;

    [Header("Turn")]
    public float turnSpeed;

    Vector3 rotation;

    [Header("Keys")]
    public KeyCode moveKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.D;
    public KeyCode rightKey = KeyCode.A;
    public KeyCode brakeKey = KeyCode.S;

    [Header("Refrences")]
    public Rigidbody rb;

    public override void OnNetworkSpawn()
    {

        index.OnValueChanged += (int previousValue, int newValue) =>
        {

            index.Value = newValue;

        };

    }

    void Start()
    {

        if (IsHost)
        {

            foreach (Vector3 index in indexer)
            {

                positions.Add(this.index.Value, index);
                this.index.Value++;

            }

            index.Value = 0;

        }

        if (IsLocalPlayer)
        {

            cam.enabled = true;

            transform.position = new Vector3(positions[index.Value].x, positions[index.Value].y, positions[index.Value].z);

            index.Value++;

        }        

        rb = GetComponent<Rigidbody>();

    }

    [ClientRpc]
    public void PositionsClientRpc(ulong id)
    {

        if (OwnerClientId == id)
        {



        }

    }

    void Update()
    {

        if (!IsOwner) return;

        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }

        if (Input.GetKey(moveKey))
        {

            rb.AddForce(-transform.forward * moveSpeed * 10, ForceMode.Force);

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
            rb.AddForce(transform.forward * moveSpeed * 5, ForceMode.Force);

        }
   
    }

}
