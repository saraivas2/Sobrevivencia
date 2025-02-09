using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    public float velocity;
    public float forcaPulo;
    private bool travarMouse = true;
    private float mouseX = 0.0f, mouseY = 0.0f;
    float sensibilidade = 1.2f;
    private Animator animator;
    private Rigidbody rb;
    private int walkPlayer = Animator.StringToHash("walking");
    private int jump = Animator.StringToHash("jump1");
    private int death = Animator.StringToHash("death");
    private int runplyer1 = Animator.StringToHash("running1");
    private int pistol = Animator.StringToHash("Pistol");
    private int rifle = Animator.StringToHash("rifleFire");
    private int TwoJump = 0;
    private bool booljump = false;

    float angleY;
    Vector3 CamVect;
    Quaternion quat;
    


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); 
        if (travarMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        
        mouseY += Input.GetAxis("Mouse X") * sensibilidade;
        
        transform.eulerAngles=new Vector3(0, mouseY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (TwoJump < 2)
            {
                booljump = true;
            }
            else 
            {
                booljump = false;
                TwoJump = 0;
            }

        }

        move();
       
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.R))
        {
            RunningPlayer();
            transform.Translate(Vector3.forward * Time.deltaTime * velocity * 2);

        } else if (Input.GetKey(KeyCode.W))
        {
            movePlayer();
            transform.Translate(Vector3.forward * Time.deltaTime * velocity);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            movePlayer();
            transform.Translate(Time.deltaTime * velocity * Vector3.back);

        }  else
        {
            IdlePlayer();
        }
    }

    private void movePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(walkPlayer, true);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void JumpPlayer()
    {
        animator.SetBool(jump, true);
        animator.SetBool(walkPlayer, true);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);

    }


    private void AttackPlayerPistol()
    {
        animator.SetBool(jump, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, true);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void AttackPlayerRifle()
    {
        animator.SetBool(jump, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, true);
        animator.SetBool(death, false);
    }

    private void DeathPlayer()
    {
        /*barraVida.SetActive(false);*/
        animator.SetBool(jump, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, true);/*
       
        bool resp = gameover.ShowTelaGameOver(true);
        
        Invoke("ReloadScene", 3f);*/

    }

    private void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void IdlePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }
    private void RunningPlayer()
    {
        animator.SetBool(runplyer1, true);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(jump, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void OnDrawGizmos()
    {
        if (this)
        {
            Gizmos.color = Color.red;
            Vector3 Position = transform.position;
            Gizmos.DrawWireSphere(Position, 15);

        }
    }
}
