using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSplitTexture : MonoBehaviour
{
    public SpriteRenderer[] m_splitRenderers;


    // Update is called once per frame
    void Update()
    {
        if(HasDisableSplitRenderer())
        {
            for(int i=0;i<GlobalMananger.isPassLevel.Length;i++)
            {
                if(GlobalMananger.isPassLevel[i]&&!m_splitRenderers[i].enabled)
                {
                    m_splitRenderers[i].enabled = true;
                }
            }
        }
    }

    bool HasDisableSplitRenderer()
    {
        for(int i=0;i<3;i++)
        {
            if(!m_splitRenderers[i].enabled)
            {
                return true;
            }
        }
        return false;
    }
}
