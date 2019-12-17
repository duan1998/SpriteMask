using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnableSplitTexture : MonoBehaviour
{
    public SpriteRenderer[] m_splitRenderers;
    public EndLevelText _endLevelText;
    public bool isEnd = false;
    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < GlobalMananger.isPassLevel.Length; i++)
        {
            if (GlobalMananger.isPassLevel[i] && GlobalMananger.needAppear != i)
            {
                m_splitRenderers[i].enabled = true;
                m_splitRenderers[i].material.DOFade(1, 0);
            }
        }
        if (GlobalMananger.needAppear < 0) return;
        Debug.Log(GlobalMananger.needAppear+" is need show");
        BGMController.Instance._audioSource.clip = BGMController.Instance._backMusic[0];
        BGMController.Instance._audioSource.Play();
        Debug.Log(BGMController.Instance._audioSource);
        m_splitRenderers[GlobalMananger.needAppear].enabled = true;
        m_splitRenderers[GlobalMananger.needAppear].DOFade(0, 0f).SetUpdate(true);
        Tweener a = m_splitRenderers[GlobalMananger.needAppear].DOFade(1, 10).SetUpdate(true);
        //预计加入渐明效果
        string ss = "End" + (GlobalMananger.needAppear + 1);
        Debug.Log(ss);
        _endLevelText = GameObject.Find(ss).GetComponent<EndLevelText>();
        _endLevelText.BeginLoadGameScene();
        GlobalMananger.partOfPicture++;
    }
    IEnumerator MakeImageEnable()
    {
        yield return null;

    }
    void Update()
    {
        if (!BGMController.Instance.isShow&& GlobalMananger.partOfPicture==3)
            if (!isEnd)
            {
                isEnd = true;
                StartCoroutine(TurnToEndPanel());
            }
    }
    IEnumerator TurnToEndPanel()
    {
        yield return null;
        OwnerTextShow _ownerText = GameObject.Find("OwnerText").GetComponent<OwnerTextShow>();
        _ownerText.FromPoint("EndTextTurn");
    }
    //bool HasDisableSplitRenderer()
    //{
    //    for(int i=0;i<3;i++)
    //    {
    //        if(!m_splitRenderers[i].enabled)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
