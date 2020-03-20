using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float rot =0f;
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
     controller = GetComponent<CharacterController>();
     anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }
    void Movement()
    {
              // if(controller.isGrounded)
        // {
            if(Input.GetKey(KeyCode.W))
            {   
                if(anim.GetBool("jump") == true)
                {
                    return;
                }
                else if(anim.GetBool("jump") == false)
                {
                anim.SetBool("walk",true);
                anim.SetInteger("condition",1);
                moveDir = new Vector3(0,0,1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                }
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("walk",false);
                anim.SetInteger("condition",0);
                moveDir = new Vector3(0,0,0);
            }
        // }
        rot += Input.GetAxis("Horizontal")* rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0,rot,0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
    void GetInput()
    {
        // if(controller.isGrounded)
        // {
            if(Input.GetKey(KeyCode.Space))
            {   if(anim.GetBool("walk") == true)
                {
                    anim.SetBool("walk",false);
                    anim.SetInteger("condition",0);
                }
                else if(anim.GetBool("walk")== false)
                {
                    Jump();
                }
                
            }
        // }
    }
    void Jump()
    {
        StartCoroutine(JumpRoutine());   
    }
    IEnumerator JumpRoutine()
    {
        anim.SetBool("jump",true);
        anim.SetInteger("condition",2);
        yield return new WaitForSeconds(2);
        anim.SetInteger("condition",0);
        anim.SetBool("jump",false);
    }
}
