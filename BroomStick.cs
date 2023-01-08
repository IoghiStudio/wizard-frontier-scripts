using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomStick : MonoBehaviour
{
    //public GameObject broomEquip;
    public GameObject broomEquipText;
    PlayerController playerScript;
    public Animator animator;
    

    private float boostSpeedMultipler = 2.5f;
    //private float broomSpeed = 6.0f;

    static float equippedBroomHeight = 10;

    //Broom Booleans
     static bool boostReady;
     bool equipped;
     bool equippable;
     static bool onBoost;
     public static bool slotFull;

    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        boostReady = true;

        //speeds
        //initialPlayerSpeed = playerScript.runningSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        //BroomMovement();
        EquipBroom();
        ExitBroom();
        BroomBoost();
    }
    /*public void BroomMovement()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();
        Vector3 broomVelocity = (transform.forward * moveDir.y + transform.right * moveDir.x) * broomSpeed;
        if (playerScript.useBroom)
        {
            playerScript.controller.Move(broomVelocity * Time.deltaTime);
        }
    }*/

    public void EquipBroom()
    {
        if (Input.GetKeyDown(KeyCode.E) && !equipped && equippable && !slotFull)
        {
            playerScript.useBroom = true;

            playerScript.controller.enabled = false;
            transform.SetParent(playerScript.broomContainer);
            playerScript.transform.position += new Vector3(0, equippedBroomHeight, 0);
            playerScript.controller.enabled = true;
            
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            /*transform.localScale = Vector3.one;*/

            slotFull = true;
            equipped = true;
            equippable = false;
            //playerScript.walkSpeed = initialPlayerSpeed * broomSpeed;
            broomEquipText.SetActive(false);
            animator.SetTrigger("onBroom");

        }
    }
    public void ExitBroom()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && equipped && !onBoost && slotFull)
        {
            playerScript.useBroom = false;

            playerScript.controller.enabled = false;
            playerScript.transform.position += new Vector3(0, -equippedBroomHeight, 0);
            playerScript.controller.enabled = true;
            transform.SetParent(null);
            broomEquipText.SetActive(false);

            equipped = false;
            slotFull = false;
            //playerScript.walkSpeed = initialPlayerSpeed;
            playerScript.exitBroomEffect.Play();

            transform.position = playerScript.transform.position /*+ new Vector3(0, -0.9f, 0)*/;
            animator.SetTrigger("onBroom");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            equippable = true;
            if(!slotFull)
            {
                broomEquipText.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            broomEquipText.SetActive(false);
            equippable = false;
        }
    }
    void BroomBoost()
    {
        if (Input.GetKeyDown(KeyCode.Q) && equipped && boostReady && playerScript.mana > 30)
        {
            playerScript.mana -= 30;
            playerScript.broomSpeed *= boostSpeedMultipler;
            boostReady = false;
            onBoost = true;
            StartCoroutine(BoostIsGone(3));

        }
    }

    IEnumerator BoostIsGone(float time)
    {
        yield return new WaitForSeconds(time);
        playerScript.broomSpeed /= boostSpeedMultipler;
        onBoost = false;
        StartCoroutine(BoostCharging(3));
    }

    IEnumerator BoostCharging(float time)
    {
        yield return new WaitForSeconds(time);
        boostReady = true;
    }
}