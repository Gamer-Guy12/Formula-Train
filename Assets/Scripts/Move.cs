using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class Move : NetworkBehaviour
{

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

    void Start()
    {

        if (IsLocalPlayer)
        {

            cam.enabled = true;

        }        

        rb = GetComponent<Rigidbody>();

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

            rb.AddForce(transform.forward * moveSpeed * 10, ForceMode.Force);

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
            rb.AddForce(-transform.forward * moveSpeed * 5, ForceMode.Force);

        }
   
    }

}
