using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator animator;
    float verticalInput;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);
        if (verticalInput < 0)
            animator.SetBool("WalkingBackwards", true);
        else
            animator.SetBool("WalkingBackwards", false);

        if(player.isRunning)
        {
            animator.SetBool("isRunning", true);
        } else
            animator.SetBool("isRunning", false);
        if (player.isDead)
            animator.SetBool("isDead", true);
    }
}