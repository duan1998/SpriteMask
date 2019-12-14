using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : BeMaskedSprite
{
    public Key m_key;
    private BoxCollider2D m_box2D;
    public AudioClip m_clip;

    private void Awake()
    {
        m_box2D = GetComponent<BoxCollider2D>();
    }

    public override void BMask()
    {
        base.BMask();
        m_box2D.enabled = true;
        
    }
    public override void NotBMask()
    {
        base.NotBMask();
        m_box2D.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&m_key.m_bGet)
        {
            AudioSource.PlayClipAtPoint(m_clip, transform.position);
            this.gameObject.SetActive(false);
        }
    }

    public override bool Check()
    {
        throw new System.NotImplementedException();
    }

 
}
