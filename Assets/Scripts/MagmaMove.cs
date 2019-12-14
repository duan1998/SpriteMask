using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaMove : MonoBehaviour
{
    public float Speed;
    Vector3 SelfPos;

    // Start is called before the first frame update
    void Start()
    {
        SelfPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime*Speed, Space.World);
    }
}
