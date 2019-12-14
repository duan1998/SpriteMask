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
    }
}
