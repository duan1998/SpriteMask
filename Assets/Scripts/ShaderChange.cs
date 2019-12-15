using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderChange : MonoBehaviour
{
    public float _threShold;
    public float _edgeLength;
    public Canvas _canvas;
    Image _Shader;
    // Start is called before the first frame update
    void Start()
    {
        _Shader = this.GetComponent<Image>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        _Shader.material.SetFloat("_Threshold",_threShold);
        _Shader.material.SetFloat("_EdgeLength", _edgeLength);
        _Shader.material.SetVector("_StartPoint",_canvas.transform.position);
    }
}
