using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block : BeMaskedSprite
{
    private BoxCollider2D m_box2D;
    private SpriteRenderer m_renderer;

    public bool m_isGreen;
    private void Awake()
    {
        m_box2D = GetComponent<BoxCollider2D>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
    }
    public override void BMask()
    {
        base.BMask();
        if (m_isGreen)
        {
            m_box2D.isTrigger = true;
            m_renderer.enabled = false;
        }
        else
        {
            m_box2D.isTrigger = false;
            m_renderer.enabled = true;
        }

    }
    public override void NotBMask()
    {
        base.NotBMask();
        if (m_isGreen)
        {
            m_box2D.isTrigger = false;
            m_renderer.enabled = true;
        }
        else
        {
            m_box2D.isTrigger = true;
            m_renderer.enabled = false;
        }
    }

    public override bool Check()
    {
        Vector3 circlePos = m_spriteMaskRenderer.transform.position;
        float circleRadio = m_spriteMaskRenderer.bounds.size.x/ 2;

        Vector3 rectPos = transform.position;
        float rectEdgeLength = m_renderer.bounds.size.x;
        float absX = Mathf.Abs(circlePos.x - rectPos.x);
        float absY = Mathf.Abs(circlePos.y - rectPos.y);
        float distance = Mathf.Sqrt((absX * absX) + (absY * absY));
        if (distance < (circleRadio + rectEdgeLength / 2)) return true;
        else return false;
    }
}
