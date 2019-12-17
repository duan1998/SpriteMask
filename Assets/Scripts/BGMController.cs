using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public bool isSceneChange = false;
    private static BGMController _instance;
    public static BGMController Instance { get { return _instance; } }
    public AudioSource _audioSource;
    public List<AudioClip> _backMusic;
    public bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = this.GetComponent<AudioSource>();
            _audioSource.clip = _backMusic[0];
            _audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
