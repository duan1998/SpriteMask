using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D m_controller;

    public float m_moveSpeed = 40f;

    public Animator m_anim;

    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * m_moveSpeed;

        m_anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            m_anim.SetBool("IsJumping", true);
        }
    }
    public void OnLanding()
     {
        m_anim.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        m_controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }
}
