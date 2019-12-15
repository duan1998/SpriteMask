using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShaderController : MonoBehaviour
{
    public ShaderChange _shaderChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _shaderChange = this.GetComponent<ShaderChange>();
        DontDestroyOnLoad(this.gameObject);
    }
    public void ToPoint()
    {
        _shaderChange._threShold = 0.3f;
        _shaderChange._edgeLength = 0f;
        StartCoroutine(ToPointIenum());
    }
    public void FromPoint()
    {
        _shaderChange._threShold = 1f;
        _shaderChange._edgeLength = 1f;
        StartCoroutine(FromPointIenum());
    }
    IEnumerator FromPointIenum()
    {
        DOTween.To(() => _shaderChange._threShold, x => _shaderChange._threShold = x, 0.3f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _shaderChange._edgeLength, x => _shaderChange._edgeLength = x, 0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator ToPointIenum()
    {
        DOTween.To(() => _shaderChange._edgeLength, x => _shaderChange._edgeLength = x, 1f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        DOTween.To(() => _shaderChange._threShold, x => _shaderChange._threShold = x, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
