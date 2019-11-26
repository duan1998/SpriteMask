using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDogSprite : MonoBehaviour
{

    EnemyMovement m_movement;

    private void Awake()
    {
        m_movement = GetComponentInParent<EnemyMovement>();
    }
    private void OnBecameVisible()
    {
        m_movement.bLock = true;

        if (transform.position.x < GameManager.Instance.m_player.transform.position.x)
        {
            m_movement.bTowardsRight = 1;

        }
        else
            m_movement.bTowardsRight = -1;
    }
}
