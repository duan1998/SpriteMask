using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SmallObjToScene : CanMutuObj
{
    public bool isCanClick=true;

    public OwnerTextShow _textShow;
    public List<OwnerText> OwnerTextList;
    public string levelName;
    public int music;
    private void OnMouseDown()
    {
        if (!BGMController.Instance.isShow)
        if (isCanClick)
        {
            isCanClick = false;
            _textShow = GameObject.Find("OwnerText").GetComponent<OwnerTextShow>();
            BeginLoadGameScene();
        }
        
    }


    public void BeginLoadGameScene()
    {

        _textShow.playerDialogue.text = "";
        _textShow.Show(OwnerTextList, levelName,music);

    }
}




