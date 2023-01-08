using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    PlayerController playerScript;

    public bool forcingWall;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*WallForcing();*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            forcingWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            forcingWall = false;
        }
    }

    void WallForcing()
    {
        if(forcingWall)
        {
            playerScript.transform.position = playerScript.transform.position;
        }
    }
}