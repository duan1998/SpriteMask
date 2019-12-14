using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool bFollow;


    private void OnEnable()
    {
        bFollow = true;
        isRun = true;
    }
    bool isRun = false;
    private void OnDrawGizmos()
    {
        if (isRun)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawCube(transform.position, GetComponent<SpriteRenderer>().bounds.size);
        }
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
