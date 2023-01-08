using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    public GameObject npcToFollow;
    Npc npcToFollowScript;
    // Start is called before the first frame update
    void Start()
    {
        npcToFollowScript = npcToFollow.GetComponent<Npc>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpawnTime(2));
        }
    }
    IEnumerator SpawnTime(float time)
    {
        yield return new WaitForSeconds(time);
        npcToFollow.SetActive(true);
        npcToFollowScript.spawnEffect.Play();
    }
}