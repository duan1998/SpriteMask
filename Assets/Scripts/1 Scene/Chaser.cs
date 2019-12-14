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
    // Start is called before the first frame update
  
    

    // Update is called once per frame
    void Update()
    {
        if(m_isWalk)
            transform.position = new Vector3(transform.position.x + m_moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if(m_isCheck)
            Check();
    }
    void Check()
    {
        RaycastHit2D hit = Physics2D.BoxCast(m_boxCenter.position, m_boxSize, 0,Vector3.right,0.1f, m_layermask);
        if (hit.collider!=null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                //追上了  重新开始
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                m_isWalk = false;
                
                //判断是否漏了出来
                //enmmmm
                if(m_endPointBlock.IsAllBlockCollider())
                {
                    //转身，。回头
                    m_isCheck = false;
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    m_moveSpeed *= -1;

                    //留下小球
                    ProducePassLevelProp();
                    m_isWalk = true;
                }
                else
                {
                    m_isWalk = false;
                    m_isCheck = false;
                    Perform();
                    //重新来
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
