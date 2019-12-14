using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CanClickObjType
{
    Guitar,
    Photo,
    Catlitter //猫窝
}

public class Guitar : BeMaskedSprite
{

    private SpriteRenderer m_renderer;
    private BoxCollider2D m_box2D;

    public CanClickObjType m_type;

    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_box2D = GetComponent<BoxCollider2D>();
    }

    public override bool Check()
    {
        return Intersects();
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

    bool Intersects()
    {
        Vector3 rect1Pos = transform.position;
        Vector3 rect2Pos = m_spriteMaskRenderer.transform.position;

        Vector2 rect1Size = m_renderer.bounds.size;
        Vector2 rect2Size = m_spriteMaskRenderer.bounds.size;

        if (Mathf.Abs(rect1Pos.x - rect2Pos.x) < (rect1Size.x + rect2Size.x) / 2 && Mathf.Abs(rect1Pos.y - rect2Pos.y) < (rect1Size.y + rect2Size.y) / 2)
            return true;
        else return false;
    }
    private void OnMouseDown()
    {
        BeginLoadGameScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene("1");
    }


    public void BeginLoadGameScene()
    {

    }
}
