using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool m_bGet=false;
    public AudioClip m_clip;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_bGet = true;
            AudioSource.PlayClipAtPoint(m_clip, transform.position);
            this.gameObject.SetActive(false);
        }
    }
}
