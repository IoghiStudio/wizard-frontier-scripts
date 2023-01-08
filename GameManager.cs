using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerController playerScript;

    public Image hpPoints;
    public Image mnPoints;

    [SerializeField] private float healthRegenPerSecond = 5;
    [SerializeField] private float manaRegenPerSecond = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        FillAllBars();
        AutoHealthRegen();
        AutoManaRegen();
    }

    void AutoHealthRegen()
    {
        if(playerScript.health < playerScript.maxHealth)
        {
            playerScript.health += healthRegenPerSecond * Time.deltaTime;
        }
    }

    void AutoManaRegen()
    {
        if(playerScript.mana < playerScript.maxMana)
        {
            playerScript.mana += manaRegenPerSecond * Time.deltaTime;
        }
    }

    void FillAllBars()
    {
        if (playerScript.health > 0)
        {
            hpPoints.fillAmount = playerScript.health / playerScript.maxHealth;
            mnPoints.fillAmount = playerScript.mana / playerScript.maxMana;
        }
    }

}