using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeMaskedSprite: MonoBehaviour
{
    public Collider2D m_collider2D;

    private SpriteMask m_spriteMask;

    protected bool m_bShow;

    public bool m_isGreen;

    protected bool isIntersect;

    private void Start()
    {
        m_spriteMask = GameManager.Instance.GetComponent<MaskCtrl>().m_maskObj.GetComponent<SpriteMask>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Check())
        {
            if(m_isGreen)
            {
                Hide();
            }
            else
                Show();
        }
        else
        {
            if (m_isGreen)
            {
                Show();
            }
            else
                Hide();
        }

    }

    bool Check()
    {
        return isIntersect;
       // Debug.Log(m_collider2D.bounds.Intersects(m_spriteMask.bounds));

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
