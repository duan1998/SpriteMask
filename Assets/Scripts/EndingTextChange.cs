using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EndingTextChange : MonoBehaviour
{
    public AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        //BGMController.Instance._audioSource.clip = BGM;
        //BGMController.Instance._audioSource.Play();
        transform.DOMove(new Vector3(960f, 2200f, 0), 23);
        StartCoroutine(Finish());
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(23f);
        this.GetComponent<Text>().DOFade(0, 3);
        //this.GetComponent<Text>().enabled=false;
            
        Debug.Log("Success Delete");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
