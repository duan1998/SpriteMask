using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chaser : MonoBehaviour
{

    public float m_moveSpeed;
    public Transform m_boxCenter;
    public Vector2 m_boxSize;
    public LayerMask m_layermask;

    public EndPointBlock m_endPointBlock;

    public GameObject m_passLevelObj;

    public bool m_isWalk;
    public bool m_isCheck;

    public float m_delayTime;
    // Start is called before the first frame update
  
    

    // Update is called once per frame
    void Update()
    {
        if(m_isWalk)
            transform.position = new Vector3(transform.position.x + m_moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if(m_isCheck)
            Check();
    }


    float timer = 0f;

    void Check()
    {
        RaycastHit2D hit = Physics2D.BoxCast(m_boxCenter.position, m_boxSize, 0,Vector3.right,0.1f, m_layermask);
        if (hit.collider!=null)
        {
            m_isWalk = false;
            if (hit.collider.CompareTag("Player"))
            {
                Perform();
                //追上了  重新开始
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else 
            {
                timer += Time.deltaTime;
                if (timer >= m_delayTime)
                {
                    //转身，。回头
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    m_moveSpeed *= -1;

                    //留下小球
                    ProducePassLevelProp();
                    m_isWalk = true;
                    timer = 0;
                }
                else
                {
                    if (!m_endPointBlock.IsAllBlockCollider())
                    {
                        m_isWalk = false;
                        m_isCheck = false;
                        Perform();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
    }


    private void Start()
    {
        m_isRun = true;
    }
    private bool m_isRun;
    private void OnDrawGizmos()
    {
        if(m_isRun)
        {
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(m_boxCenter.position, m_boxSize);
        }
    }

    //行为艺术
    void Perform()
    {
        
    }

    void ProducePassLevelProp()
    {
        m_passLevelObj.SetActive(true);
    }

   
        
    
    

}
