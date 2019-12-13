using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeMaskedSprite: MonoBehaviour
{
    public Collider2D m_collider2D;

    protected SpriteRenderer m_spriteMaskRenderer;

    protected bool m_bMask=false;

    protected bool isIntersect;

    private BlockRoot m_root;

    private void Start()
    {
        m_spriteMaskRenderer = GameManager.Instance.GetComponent<MaskCtrl>().m_maskObj.GetComponent<SpriteRenderer>();
        m_root = GetComponentInParent<BlockRoot>();
    }


    // Update is called once per frame
    void Update()
    {
        if(m_bMask&& !Check())
        {
            
            NotBMask();
        }
        if(m_root.isIntersect)
        {
            if(Check())
            {
                
                BMask();
            }
        }


        
        
    }

    public abstract bool Check();
    


    public virtual void BMask()
    {
        m_bMask = true;
    }
    public virtual void NotBMask()
    {
        m_bMask = false;
    }

    
}
