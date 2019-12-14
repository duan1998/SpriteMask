using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float Speed;
    Rigidbody2D mPlay_Rig;

    // Start is called before the first frame update
    void Start()
    {
        mPlay_Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = mPlay_Rig.velocity.magnitude;
        if (Speed > 40.0f)
            Destroy(this.gameObject);
    }
}
