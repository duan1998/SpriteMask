using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public CharacterController2D m_controller;
    public Transform m_obstacleCheck;

    public float m_moveSpeed = 40f;

    public Animator m_anim;

    public Vector2 m_boxSize;
    public LayerMask m_groundMask;

    public int bTowardsRight=-1;

    // Start is called before the first frame update
    void Start()
    {
        bRun = true;
    }

    public bool bLock=false;
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (bLock)
        {
            m_controller.Move(Mathf.Sign(bTowardsRight) * m_moveSpeed * Time.deltaTime, false, false);
        }
    }


    private void Update()
    {
        RaycastHit2D hit2D= Physics2D.BoxCast(m_obstacleCheck.position, m_boxSize, 0, new Vector2(bTowardsRight, 0),0.05f,m_groundMask);
        if (hit2D.collider!=null)
        {
            bTowardsRight *=-1;
        }
    }

    bool bRun=false;
    private void OnDrawGizmos()
    {
        if(bRun)
        {
            Gizmos.DrawWireCube(m_obstacleCheck.position, m_boxSize);
        }
    }

}
