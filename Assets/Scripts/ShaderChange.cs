using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderChange : MonoBehaviour
{
    public float _threShold;
    public float _edgeLength;
    Material _Shader;
    // Start is called before the first frame update
    void Start()
    {
        _Shader = this.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        _Shader.SetFloat("_Threshold",_threShold);
        _Shader.SetFloat("_EdgeLength", _edgeLength);
    }
}
