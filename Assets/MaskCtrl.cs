using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    public GameObject m_maskObj;

    public void EnableMaskObj()
    {
        m_maskObj.GetComponent<FollowMouse>().enabled = true;
        m_maskObj.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void DisableMaskObj()
    {
        m_maskObj.GetComponent<FollowMouse>().enabled = false;
        m_maskObj.GetComponent<SpriteRenderer>().enabled = false;
    }
}
