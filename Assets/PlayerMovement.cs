﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D m_controller;

    public float m_moveSpeed = 40f;

    public Animator m_anim;

    float horizontalMove = 0f;
    bool jump = false;
    Rigidbody2D rig;
    [SerializeField]
    Camera Main;
    public Transform target;
    [SerializeField]
    GameObject Cat;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //死亡
            Dead();
        }
        if (collision.CompareTag("win"))
        {

            GlobalMananger.isPassLevel[(SceneManager.GetActiveScene().buildIndex - 1)] = true;
            //场景切换
            ChangeSence();
        }
        if (collision.CompareTag("Cat"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name == "CheckOut")
        {
            //人物X轴锁死
            LockPosX();
            //镜头移动
            MoveCamera();
            //猫出现
            Cat.SetActive(true);
        }
    }

    public void LockPosX()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezeRotation;
    }

    public void UnLockPosX()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.constraints -= RigidbodyConstraints2D.FreezePositionX;
    }

    private void MoveCamera()
    {
        Main.orthographicSize = 8.4f;
        FollowPlayer CameraController = Main.GetComponent<FollowPlayer>();
        CameraController.isFollowY = true;
        CameraController.m_player = target;
        CameraController.m_smooth = 0.04f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //死亡
            Dead();
        }
        if (collision.collider.tag == "win")
        {

            GlobalMananger.isPassLevel[(SceneManager.GetActiveScene().buildIndex - 1)] = true;
            //场景切换
            ChangeSence();
        }
    }

    private void ChangeSence()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {        
        //PlayDeadVideo();
        ReStart();
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    private void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //播放死亡动画
    private void PlayDeadVideo()
    {
        throw new NotImplementedException();
    }
}
