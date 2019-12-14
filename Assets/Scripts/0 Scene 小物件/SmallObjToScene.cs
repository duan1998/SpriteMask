using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SmallObjToScene : CanMutuObj
{
    

    private void OnMouseDown()
    {
        BeginLoadGameScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(((int)m_type).ToString());
    }


    public void BeginLoadGameScene()
    {

    }
}




