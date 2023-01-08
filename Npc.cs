using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Npc : MonoBehaviour
{
    //public Image npcDialogBG;
    public TextMeshProUGUI npcDialogText;
    public GameObject images;
    public GameObject followButton;
    public AudioSource npcAudioSource;
    public AudioClip npcAudioClip;
    public AudioClip npcAudioClip2;
    public AudioClip npcLaught;
    public ParticleSystem meetingEffect;
    public ParticleSystem walkingEffect;
    public ParticleSystem spawnEffect;
    //public Button followButton;
    public float speed = 10.0f;
    //public float timeToWalk = 10.0f;
    //Vector3 wayPoint = new Vector3(20, 0, 0);
    public GameObject wayPoint1;
    public bool readyToGo = false;
    public bool firstWalk;

    // Start is called before the first frame update
    void Start()
    {
        //npcDialogBG.enabled = false;
        //npcDialogText.enabled = false;
        images.gameObject.SetActive(false);
        followButton.gameObject.SetActive(false);
        
        npcAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToGo == true)
            transform.position = Vector3.MoveTowards(transform.position, wayPoint1.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            if(!firstWalk)
            {
                StartCoroutine(waitRandom(1));
            }
        }
    }
    IEnumerator waitForDisable()
    {
        yield return new WaitForSeconds(3);
        // npcDialogBG.enabled = false;
        //npcDialogText.enabled = false;
        images.gameObject.SetActive(false);
        meetingEffect.Stop();
        StartCoroutine(waitForDisable2(3));
    }


    IEnumerator waitRandom(float time)
    {
        //npcDialogBG.enabled = true;
        //npcDialogText.enabled = true;
        yield return new WaitForSeconds(time);
        images.gameObject.SetActive(true);
        StartCoroutine(waitForDisable());
        npcAudioSource.PlayOneShot(npcAudioClip);
        npcAudioSource.PlayOneShot(npcAudioClip2, 0.5f);
        meetingEffect.Play();
        followButton.gameObject.SetActive(true);
    }
    IEnumerator waitForDisable2 (float time)
    {
        yield return new WaitForSeconds(time);
        followButton.gameObject.SetActive(false);
    }
    IEnumerator NpcLaughtTime(float time)
    {
        yield return new WaitForSeconds(time);
        npcAudioSource.PlayOneShot(npcLaught);
    }
    public void FollowNpc()
    {
        readyToGo = true;
        followButton.gameObject.SetActive(false);
        images.gameObject.SetActive(false);
        npcAudioSource.Stop();
        meetingEffect.Stop();
        StartCoroutine(NpcLaughtTime(5));
        walkingEffect.Play();
        if(transform.position == wayPoint1.transform.position)
        {
            walkingEffect.Stop();
        }
        firstWalk = true;
    }

    
    
}