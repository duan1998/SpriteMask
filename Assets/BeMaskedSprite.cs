using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeMaskedSprite: MonoBehaviour
{

    protected SpriteRenderer m_spriteMaskRenderer;

    protected bool m_bMask=false;

    protected bool isIntersect;

    private BlockRoot m_root;

    [SerializeField]
    private bool m_hasRoot=true;

    private void Start()
    {
        m_spriteMaskRenderer = GameManager.Instance.GetComponent<MaskCtrl>().m_maskObj.GetComponent<SpriteRenderer>();
        if(m_hasRoot)
            m_root = GetComponentInParent<BlockRoot>();
    }


    // Update is called once per frame
    void Update()
    {
        if(m_spriteMaskRenderer.enabled==false&&m_bMask)
        {
            NotBMask();
        }
        else
        {
            if (m_bMask && !Check())
            {

                NotBMask();
            }

            if (m_hasRoot && m_root.isIntersect || !m_hasRoot)
            {
                if (Check())
                {
                    BMask();
                }
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
