using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    public GameObject m_maskObj;

    public void EnableMaskObj()
    {
        m_maskObj.SetActive(true);
    }
    public void DisableMaskObj()
    {
        m_maskObj.SetActive(false);
    }
}
