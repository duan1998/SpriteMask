using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOwnerText : MonoBehaviour
{
    public OwnerTextShow _ownerText;
    // Start is called before the first frame update
    void Start()
    {
        _ownerText.Show(_ownerText._myText, "0");
        Debug.Log("Success Income");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
