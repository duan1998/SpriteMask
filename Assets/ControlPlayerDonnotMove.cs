using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerDonnotMove : MonoBehaviour
{
    public PlayerMovement m_player;

    public float m_controlTime;

    // Start is called before the first frame update
    void Start()
    {
        m_player.LockPosX();
        Invoke("UnlockPlayer", m_controlTime);
    }

    void UnlockPlayer()
    {
        m_player.UnLockPosX();
    }


}
