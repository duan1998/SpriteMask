using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct TextSimple
{
    public bool isMe;//是否为自己的对话框
    public string TextToBox;//需要添加的文本
    public List<TextOfMe> myText;//如果是自己的对话框，调整打字效果
    public float timeOfPause;//到下一段的延迟时间
    public AudioClip changeEffect;//发出文本时音效
}
[System.Serializable]
public struct TextOfMe
{
    public bool isAdd;//添加还是删除
    public string text;//需要添加的文本段，若删除则不用填
    public float timeOfEachWord;//添加删除每个字符的时间
    public float timeToNextPart;//到下一段的延迟时间
    public int numberOfDelete;//若删除，删除字符的个数
    public AudioClip eachWordEffect;//敲击单词声（忽略空格
}
public class DialogueController : MonoBehaviour
{
    public AudioSource wordAudio;
    public AudioClip defaultWordEffect;//默认敲击音效
    public AudioClip defaultEnterEffect;//默认切换音效
    public GameObject _textBoxOfMe;
    public GameObject _textBoxOfOther;
    public GameObject DialoguePanel;
    public ScrollRect scrollRect;
    public Text playerDialogue;
    private bool scrollToTop;

    public List<TextSimple> _allText;
    public List<TextOfMe> _myText;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        scrollToTop = false;
        wordAudio = GetComponent<AudioSource>();
        if (defaultWordEffect!=null)
        wordAudio.clip = defaultWordEffect;

        scrollRect.onValueChanged.AddListener(delegate (Vector2 v)
        {
            Debug.Log(v);
        }
        );
    }
    // Update is called once per frame
    void Update()
    {
        if (scrollToTop)
        scrollRect.verticalNormalizedPosition = 0;
    }
    public void Show()
    {
        StartCoroutine(DialogueStart(_allText));
        //StartCoroutine(ShowMyText(_myText));

    }
    IEnumerator DialogueStart(List<TextSimple> allText)
    {
        int n = allText.Count;
        for (int i = 0; i < allText.Count; i++)
        {
            if (allText[i].isMe)
            {
                yield return StartCoroutine(ShowMyText(allText[i].myText));
                if (allText[i].changeEffect != null) wordAudio.clip = allText[i].changeEffect;
                else if (defaultEnterEffect != null) wordAudio.clip = defaultEnterEffect;
                wordAudio.Play();
                AddTextToWindow(true,allText[i].TextToBox);
            }
            else AddTextToWindow(false, allText[i].TextToBox);

            yield return new WaitForSeconds(allText[i].timeOfPause);
        }
    }
    IEnumerator ShowMyText(List<TextOfMe> myText)
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
                    playerDialogue.text += myText[i].text[j];
                    yield return new WaitForSeconds(myText[i].timeOfEachWord);
                }
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
            
        }
        //此处添加文本到layout中并清除text文本;
    }
    public void AddTextToWindow(bool isMe,string text)
    {
        playerDialogue.text = "";
        if (isMe)
        {
            GameObject addBox = Instantiate(_textBoxOfMe);
            addBox.transform.parent = DialoguePanel.transform;
            addBox.GetComponentInChildren<Text>().text = text;
        }
        else
        {
            GameObject addBox = Instantiate(_textBoxOfOther);
            addBox.transform.parent = DialoguePanel.transform;
            addBox.GetComponentInChildren<Text>().text = text;
        }
        StartCoroutine("MoveBox");
    }
    IEnumerator MoveBox()
    {
        scrollToTop = true;
        yield return new WaitForSeconds(0.5f);
        scrollToTop = false;
    }
}
