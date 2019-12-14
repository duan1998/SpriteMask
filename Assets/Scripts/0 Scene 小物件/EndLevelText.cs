using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelText : MonoBehaviour
{
    public OwnerTextShow _textShow;
    public List<OwnerText> OwnerTextList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BeginLoadGameScene()
    {
        _textShow = GameObject.Find("OwnerText").GetComponent<OwnerTextShow>();
        _textShow.playerDialogue.text = "";
        _textShow.Show(OwnerTextList);
    }

}
