using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInteractFalse : MonoBehaviour
{
    public GameObject[] smallObj;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GlobalMananger.isPassLevel.Length; i++)
        {
            if (GlobalMananger.isPassLevel[i]) smallObj[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
