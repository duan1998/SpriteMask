using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool bFollow;

    // Start is called before the first frame update
    void Start()
    {
        bFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!bFollow)
            {
                bFollow = true; 
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if(bFollow)
            {
                bFollow = false;
            }
        }
        if(bFollow)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}
