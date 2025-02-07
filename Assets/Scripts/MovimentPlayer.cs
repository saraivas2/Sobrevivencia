using System.Collections;
using System.Collections.Generic;
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
    private int idleHash = Animator.StringToHash("idle");
    private int runplyer1 = Animator.StringToHash("running1");
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
        mouseX += Input.GetAxis("Mouse Y") * sensibilidade;

        transform.eulerAngles = new Vector3(-mouseX, mouseY, 0);

        angleY = transform.eulerAngles.y;
             
        CamVect = GetDirectionVector(angleY);

        move();
       
    }

    private void move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
            rb.AddForce(Vector3.up *  forcaPulo * Time.deltaTime, ForceMode.Impulse);
        
        } else if (Input.GetKey(KeyCode.W))
        {
            movePlayer();
            transform.Translate(Vector3.forward * Time.deltaTime * velocity);

        } else if (Input.GetKey(KeyCode.S))
        {
            movePlayer();
            transform.Translate(Time.deltaTime * velocity * Vector3.back);

        } else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Time.deltaTime * velocity * Vector3.left);

        } else  if (Input.GetKey(KeyCode.D))
        {
            movePlayer();
            transform.Translate(Time.deltaTime * velocity * Vector3.right);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            RunningPlayer();
            transform.Translate(Vector3.forward * Time.deltaTime * velocity * 2);
        }
        else
        {
            IdlePlayer();
            transform.Translate(Vector3.zero);
        }
    }

    private void movePlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idleHash, false);
        animator.SetBool(walkPlayer, true);
        animator.SetBool(runplyer1, false);
    }

    private void JumpPlayer()
    {
        animator.SetBool(jump, true);
        animator.SetBool(idleHash, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);

    }


    private void AttackPlayer()
    {
        /*animator.SetBool(jump, false);
        animator.SetBool(idleHash, true);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);*/

    }


    private void DeathPlayer()
    {
        /*barraVida.SetActive(false);
        animator.SetBool(jump, false);
        animator.SetBool(idleHash, true);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
       
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
        animator.SetBool(idleHash, true);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
    }
    private void RunningPlayer()
    {
        animator.SetBool(jump, false);
        animator.SetBool(idleHash, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, true);
    }

    Vector3 GetDirectionVector(float angleY)
    {
        float radianY = angleY * Mathf.Deg2Rad;

        float x = Mathf.Sin(radianY);
        float z = Mathf.Cos(radianY);

        return new Vector3(x, 0, z);
    }
}
