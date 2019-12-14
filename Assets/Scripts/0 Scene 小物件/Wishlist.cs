using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wishlist : CanMutuObj
{
    public OwnerTextShow _textShow;
    public List<OwnerText> OwnerTextList;
    private void OnMouseDown()
    {

        Debug.Log("按到了愿望单");
        _textShow = GameObject.Find("OwnerText").GetComponent<OwnerTextShow>();
        _textShow.playerDialogue.text = "";
        _textShow.Show(OwnerTextList);
    }
}