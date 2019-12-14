using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SmallObjToScene : CanMutuObj
{
    public bool isCanClick=true;

    public OwnerTextShow _textShow;
    public List<OwnerText> OwnerTextList;
    public string levelName;
    private void OnMouseDown()
    {
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
        _textShow.Show(OwnerTextList, levelName);
    }
}




