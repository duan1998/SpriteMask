using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointBlock : MonoBehaviour
{
    public Block[] m_blocks;

    public bool IsAllBlockCollider()
    {
        for(int i=0;i<m_blocks.Length;i++)
        {
            if (m_blocks[i].m_box2D.isTrigger)
                return false;
        }
        return true;
    }
}
