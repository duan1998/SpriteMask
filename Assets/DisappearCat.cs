using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearCat : MonoBehaviour
{
    public Animator m_anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_anim.Play("dispear", 0);
        }
    }
    void OnCatDispearAnimationEndEvent()
    {
        Destroy(this.gameObject);
    }
}
