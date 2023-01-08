using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    PlayerController playerScript;
    public bool onFire;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TakingDamage();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onFire = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onFire = false;
    }

    void TakingDamage()
    {
        if(onFire)
        {
            playerScript.health -= 20 * Time.deltaTime;
        }
    }
}