using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EndingImageController : MonoBehaviour
{
    public List<Image> _imageList;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _imageList[i].DOFade(0, 0f);
        }
        StartCoroutine(ImageShow());
    }
    IEnumerator ImageShow()
    {
        _imageList[0].DOFade(1, 5f);
        yield return new WaitForSeconds(4);
        _imageList[0].DOFade(0,3f);
        yield return new WaitForSeconds(1);
        _imageList[1].DOFade(1, 5f);
        yield return new WaitForSeconds(4);
        _imageList[1].DOFade(0, 3f);
        yield return new WaitForSeconds(1);
        _imageList[2].DOFade(1, 5f);
        yield return new WaitForSeconds(4);
        _imageList[2].DOFade(0, 3f);
        yield return new WaitForSeconds(1);
        _imageList[3].DOFade(1, 5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
