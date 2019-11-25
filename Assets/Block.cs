using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BeMaskedSprite
{
    private BoxCollider2D m_box2D;

    private void Awake()
    {
        m_box2D = GetComponent<BoxCollider2D>();
    }

    public override void Show()
    {
        base.Show();
        m_box2D.isTrigger = false;
    }
    public override void Hide()
    {
        base.Hide();
        m_box2D.isTrigger = true;
    }
}
