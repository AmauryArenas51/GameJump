using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Impulse : MonoBehaviour
{
    private Vector3 startPos;
    public float floatForce;
    public float fallForce;

    private PlayerControllerX playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();

        startPos = transform.position; // Establish the default starting position 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && startPos.y > 18)
        {
            //playerControllerScript.gameObject.transform.position = new Vector3(transform.position.x, 18, transform.position.z);
            playerControllerScript.PlayerRb.AddForce(Vector3.down * fallForce, ForceMode.Impulse);
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.bouncingSound, 1.5f);

        }
        else if (collision.gameObject.CompareTag("Player") && startPos.y < 1)
        {
            //playerControllerScript.gameObject.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            playerControllerScript.PlayerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            playerControllerScript.playerAudio.PlayOneShot(playerControllerScript.bouncingSound, 1.5f);
        }
    }
}
