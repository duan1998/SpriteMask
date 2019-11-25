using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeMaskedSprite: MonoBehaviour
{
    public Collider2D m_collider2D;

    public SpriteMask m_spriteMask;

    protected bool m_bShow;


    // Update is called once per frame
    void Update()
    {
        if(Check())
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    bool Check()
    {
        return m_collider2D.bounds.Intersects(m_spriteMask.bounds);
    }

    public virtual void Show()
    {
        m_bShow = true;
    }
    public virtual void Hide()
    {
        m_bShow = false;
    }

    
}
