using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRoot : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private SpriteRenderer m_spriteMaskRenderer;

    public bool isIntersect;
    private void Awake()
    {
        
        m_renderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isIntersect = false;
        m_spriteMaskRenderer = GameManager.Instance.GetComponent<MaskCtrl>().m_maskObj.GetComponent<SpriteRenderer>();
        isRun = true;
    }
    bool isRun = false;
    private void OnDrawGizmos()
    {
        if(isRun)
        {
            //Gizmos.color = Color.red;
            //izmos.DrawCube(transform.position, m_renderer.bounds.size);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isIntersect && Intersects())
        {
            isIntersect = true;
        }
        else if (isIntersect && !Intersects())
        {
            isIntersect = false;
        }
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

}
