using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    public ScreenEffect _screenEffect;
    public Shake _shake;
    public List<OwnerText> _myText;
    public ShaderChange _shaderChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
       // wordAudio = GetComponent<AudioSource>();
        if (defaultWordEffect!=null)
        wordAudio.clip = defaultWordEffect;
        _screenEffect = GameObject.Find("Main Camera").GetComponent<ScreenEffect>();
        _shake = GameObject.Find("Main Camera").GetComponent<Shake>();
        _shaderChange = GameObject.Find("WhiteImage").GetComponent<ShaderChange>();
        _screenEffect.enabled = false;
        _shake.enabled = false;
        //_shaderChange = GameObject.Find("WhiteImage").GetComponent<ShaderChange>();
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
    public void Show(List<OwnerText> showText, string sceneName,int musicNumber)
    {
        StartCoroutine(WaitTextFinish(showText,sceneName,musicNumber));
    }
    public void Show(List<OwnerText> showText, string sceneName,bool needLight)
    {
        StartCoroutine(WaitFirstText(showText, sceneName));
    }
    public void ToPoint()
    {
        _shaderChange._threShold = 0.3f;
        _shaderChange._edgeLength = 0f;
        StartCoroutine(ToPointIenum());
    }
    public void FromPoint()
    {
        _shaderChange._threShold = 1f;
        _shaderChange._edgeLength = 1f;
        StartCoroutine(FromPointIenum());
    }
    IEnumerator WaitFirstText(List<OwnerText> myText, string sceneName)
    {
        yield return StartCoroutine(ShowMyText(myText));
        _shaderChange._threShold = 1;
        _shaderChange._edgeLength = 1;
        DOTween.To(() => _shaderChange._threShold, x => _shaderChange._threShold = x, 0.3f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _shaderChange._edgeLength, x => _shaderChange._edgeLength = x, 0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    IEnumerator FromPointIenum()
    {
        DOTween.To(() => _shaderChange._threShold, x => _shaderChange._threShold = x, 0.3f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _shaderChange._edgeLength, x => _shaderChange._edgeLength = x, 0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator ToPointIenum()
    {
        DOTween.To(() => _shaderChange._edgeLength, x => _shaderChange._edgeLength = x, 1f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        DOTween.To(() => _shaderChange._threShold, x => _shaderChange._threShold = x, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator WaitTextFinish(List<OwnerText> myText,string sceneName,int musicNumber)
    {
        BGMController.Instance.isShow = true;
        yield return StartCoroutine(ShowMyText(myText));

        _shake.enabled = true;
        _shake.shakeCamera();
        _screenEffect.enabled = true;
        _screenEffect.StartEffect();
        yield return new WaitForSeconds(1.5f);

        _shake.enabled = false;
        _screenEffect.enabled = false;

        yield return StartCoroutine(FromPointIenum());
        BGMController.Instance._audioSource.clip = BGMController.Instance._backMusic[musicNumber];
        BGMController.Instance._audioSource.Play();
        BGMController.Instance.isShow = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    IEnumerator ShowMyText(List<OwnerText> myText)
    {
        BGMController.Instance.isShow = true;
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
        BGMController.Instance.isShow = false;
    }
}
