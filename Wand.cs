using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    PlayerController playerScript;
    public GameObject wandEquipText;
    public Transform wandContainer;

    public GameObject firstSpell;
    

    bool equippable;
    bool equipped;
    public static bool slotFull;
    bool spellReady;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        EquipWand();
        ExitWand();
        FirstSpell();
    }
    void EquipWand()
    {
        if(Input.GetKeyDown(KeyCode.E) && equippable && !equipped && !slotFull)
        {
            transform.SetParent(wandContainer);
            /*transform.SetParent(wandContainer);*/
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            equippable = false;
            equipped = true;
            slotFull = true;
            wandEquipText.SetActive(false);
            spellReady = true;
        }
          
    }
    void ExitWand()
    {
        if(Input.GetKeyDown(KeyCode.R) && equipped && slotFull)
        {
            transform.SetParent(null);
            equipped = false;
            wandEquipText.SetActive(false);
            slotFull = false;
            spellReady = false;
        }
    }
    void FirstSpell()
    {
        if(Input.GetMouseButtonDown(0) && spellReady && playerScript.mana > 10)
        {
            Instantiate(firstSpell, wandContainer.transform.position + new Vector3(0,0.5f ,0), playerScript.transform.rotation);
            playerScript.mana -= 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            equippable = true;
            if(!slotFull)
            {
                wandEquipText.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            equippable = false;
            wandEquipText.SetActive(false);
        }
    }
}