using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipDelayPlay : MonoBehaviour
{
    private AudioSource m_audio;
    public float m_delayTime;
    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_audio.PlayDelayed(m_delayTime);
    }

}
