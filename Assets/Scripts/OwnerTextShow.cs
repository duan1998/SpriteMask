using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct OwnerText
{
    public bool isAdd;//添加还是删除
    public string text;//需要添加的文本段，若删除则不用填
    public float timeOfEachWord;//添加删除每个字符的时间
    public float timeToNextPart;//到下一段的延迟时间
    public int numberOfDelete;//若删除，删除字符的个数
    public AudioClip eachWordEffect;//敲击单词声（忽略空格
    public bool needEnter;//是否需要换行
    public bool needDelete;
    public int color;//0为默认，1为红色，2为蓝色
}
public class OwnerTextShow : MonoBehaviour
{
    public AudioSource wordAudio;
    public AudioClip defaultWordEffect;//默认敲击音效
    public Text playerDialogue;

    public List<OwnerText> _myText;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
       // wordAudio = GetComponent<AudioSource>();
        if (defaultWordEffect!=null)
        wordAudio.clip = defaultWordEffect;
        Show();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Show()
    {
        StartCoroutine(ShowMyText(_myText));
    }
    public void Show(List<OwnerText> showText)
    {
        StartCoroutine(ShowMyText(showText));
    }
    public void Show(List<OwnerText> showText, string sceneName)
    {
        StartCoroutine(WaitTextFinish(showText,sceneName));
    }
    IEnumerator WaitTextFinish(List<OwnerText> myText,string sceneName)
    {
        yield return StartCoroutine(ShowMyText(myText));
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    IEnumerator ShowMyText(List<OwnerText> myText)
    {
        for (int i = 0; i < myText.Count; i++)
        {
            if (myText[i].eachWordEffect != null) wordAudio.clip = myText[i].eachWordEffect;
            else if (defaultWordEffect != null) wordAudio.clip = defaultWordEffect;
            if (myText[i].isAdd)
            {

                for (int j = 0; j < myText[i].text.Length; j++)
                {
                    if (myText[i].text[j]!=' ')
                    wordAudio.Play();
                    if (myText[i].color == 0) playerDialogue.text += myText[i].text[j];
                    if (myText[i].color == 1)
                    {
                        if (j == 0)
                        {
                            playerDialogue.text+= "<color=red>";
                            playerDialogue.text += myText[i].text[0];
                            playerDialogue.text += "</color>";
                        }
                        else
                        {
                            playerDialogue.text = playerDialogue.text.Substring(0, playerDialogue.text.Length - 19 - j);
                            playerDialogue.text += "<color=red>";
                            for (int k=0;k<=j;k++) playerDialogue.text += myText[i].text[k];
                            playerDialogue.text += "</color>";
                        }
                    }
                    if (myText[i].color == 2)
                    {
                        if (j == 0)
                        {
                            playerDialogue.text += "<color=blue>";
                            playerDialogue.text += myText[i].text[0];
                            playerDialogue.text += "</color>";
                        }
                        else
                        {
                            playerDialogue.text = playerDialogue.text.Substring(0, playerDialogue.text.Length - 20 - j);
                            playerDialogue.text += "<color=blue>";
                            for (int k = 0; k <= j; k++) playerDialogue.text += myText[i].text[k];
                            playerDialogue.text += "</color>";
                        }
                    }
                    //if (myText[i].color == 1) playerDialogue.text += "<color=red>";
                    //if (myText[i].color == 2) playerDialogue.text += "<color=blue>";
                    //playerDialogue.text += myText[i].text[j];
                    //if (myText[i].color == 1) playerDialogue.text += "</color>";
                    //if (myText[i].color == 2) playerDialogue.text += "</color>";
                    yield return new WaitForSeconds(myText[i].timeOfEachWord);

                }

                if (myText[i].needEnter)  playerDialogue.text += "\n";

                //playerDialogue.text = playerDialogue.text.Replace("\\n", "\n");
            }
            else
            {
                for(int j = 0; j < myText[i].numberOfDelete; j++)
                {
                    //token.Substring
                    wordAudio.Play();
                    playerDialogue.text = playerDialogue.text.Substring(0, playerDialogue.text.Length - 1);
                    if (playerDialogue.text == "") break;
                    yield return new WaitForSeconds(myText[i].timeOfEachWord);
                }
            }
            yield return new WaitForSeconds(myText[i].timeToNextPart);
            if (myText[i].needDelete) playerDialogue.text = "";
        }
        //此处添加文本到layout中并清除text文本;
    }
}
