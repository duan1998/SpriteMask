using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform m_player;
    [Range(0.1f,0.9f)]
    public float m_smooth;

    public bool isFollowX;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPosition= m_player.TransformPoint(new Vector3(0, 0, 0));
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowX)
            transform.position=Vector3.Lerp(
                transform.position,
                new Vector3(m_player.position.x,transform.position.y,transform.position.z), 
                m_smooth);
        else if( m_player.position.y >= transform.position.y )
            transform.position = Vector3.Lerp(
                transform.position, 
                new Vector3(transform.position.x, m_player.position.y, transform.position.z),
                m_smooth);
    }
}
