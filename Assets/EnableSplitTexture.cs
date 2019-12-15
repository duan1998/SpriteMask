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
    void Update()
    {
        if(HasDisableSplitRenderer())
        {
            for(int i=0;i<GlobalMananger.isPassLevel.Length;i++)
            {
                if (GlobalMananger.isPerformLevel[i]) m_splitRenderers[i].enabled = true;
                if(GlobalMananger.isPassLevel[i]&&!GlobalMananger.isPerformLevel[i])
                {
                    BGMController.Instance._audioSource.clip = BGMController.Instance._backMusic[0];
                    BGMController.Instance._audioSource.Play();
                    Debug.Log(BGMController.Instance._audioSource);
                    m_splitRenderers[i].enabled = true;
                    GlobalMananger.isPerformLevel[i] = true;
                    m_splitRenderers[i].material.DOFade(0,0);
                    m_splitRenderers[i].material.DOFade(1, 10);
                    //预计加入渐明效果
                    string ss = "End" + (i+1);
                    Debug.Log(ss);
                    _endLevelText = GameObject.Find(ss).GetComponent<EndLevelText>();
                    _endLevelText.BeginLoadGameScene();
                }
            }
        }
        else
        {
            if (!isEnd)
            {
                isEnd = true;
                StartCoroutine(TurnToEndPanel());
            }
        }
    }
    IEnumerator TurnToEndPanel()
    {
        yield return new WaitForSeconds(15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndTextTurn");
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
