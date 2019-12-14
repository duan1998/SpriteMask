using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
    #region Variables
    public Shader shader = null;
    private Material _material = null;
    //[Range(0, 0.07f)]
    public float intensityChangeRate = 0.007f;
   // [Range(0.01f, 1f)]
    public float maxIntensity = 1;

    [Range(0, 2)]
    public float valueChangeDurationIn = 1;
    [Range(0, 2)]
    public float valueChangeDurationOut = 1;
    [Range(-3, 3)]
    public float targetValue = 2;

   // [SerializeField, Range(0, 3)]
    public float brightnessAmount = 1.0f;
   // [SerializeField, Range(0, 3)]
    public float saturationAmount = 1.0f;
   // [SerializeField, Range(0, 3)]
    public float contrastAmount = 1.0f;
   
    float _intensity = 0;
    
    public float intensity
    {
        get { return _intensity; }
        set { _intensity = value; }
    }

    bool isTrue = false;
    bool isReach = false;
    Texture2D _noiseTexture;
    RenderTexture _trashFrame1;
    RenderTexture _trashFrame2;
    #endregion
    public Material _Material
    {
        get
        {
            if (_material == null)
                _material = GenerateMaterial(shader);
            return _material;
        }
    }
    protected Material GenerateMaterial(Shader shader)
    {
        if (shader == null)
            return null;
        if (shader.isSupported == false)
            return null;
        Material material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        SetUp();
        if (material)
            return material;
        return null;
    }
    static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, Random.value);
    }
    void SetUp()
    {
        _noiseTexture = new Texture2D(64, 32, TextureFormat.ARGB32, false);
        _noiseTexture.hideFlags = HideFlags.DontSave;
        _noiseTexture.wrapMode = TextureWrapMode.Clamp;
        _noiseTexture.filterMode = FilterMode.Point;

        _trashFrame1 = new RenderTexture(Screen.width, Screen.height, 0);
        _trashFrame2 = new RenderTexture(Screen.width, Screen.height, 0);
        _trashFrame1.hideFlags = HideFlags.DontSave;
        _trashFrame2.hideFlags = HideFlags.DontSave;
        UpdateNoiseTexture();
    }
    void UpdateNoiseTexture()
    {
        var color = RandomColor();

        for (var y = 0; y < _noiseTexture.height; y++)
        {
            for (var x = 0; x < _noiseTexture.width; x++)
            {
                if (Random.value > 0.89f) color = RandomColor();
                _noiseTexture.SetPixel(x, y, color);
            }
        }

        _noiseTexture.Apply();
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _Material.SetFloat("_brightnessAmount", brightnessAmount);
        _Material.SetFloat("_saturationAmount", saturationAmount);
        _Material.SetFloat("_contrastAmount", contrastAmount);
        var fcount = Time.frameCount;
        if (fcount % 13 == 0) Graphics.Blit(source, _trashFrame1);
        if (fcount % 73 == 0) Graphics.Blit(source, _trashFrame2);
        _material.SetFloat("_Intensity", _intensity);
        _material.SetTexture("_NoiseTex", _noiseTexture);
        var trashFrame = Random.value > 0.5f ? _trashFrame1 : _trashFrame2;
        _material.SetTexture("_TrashTex", trashFrame);
        Graphics.Blit(source, destination, _Material);
    }
    private void OnDisable()
    {
        if (_material)
        {
            DestroyImmediate(_material);
        }
    }
    void Update()
    {
        if (Random.value > Mathf.Lerp(0.9f, 0.5f, _intensity))
        {
            SetUp();
            UpdateNoiseTexture();
        }
        if (isTrue)
        {
            //Debug.Log("true");
            if (!isReach)
            {
                _intensity += intensityChangeRate;
            }
            else
            {
                _intensity -= intensityChangeRate;
            }
            if (_intensity >= maxIntensity) isReach = true;
            if (_intensity <= 0) Close();

        }
    }
    public void StartEffect()
    {
            isTrue = true;
    }
    

    public IEnumerator EndEffect()
    {
        //StartCoroutine(SetFull(duration));
        while(_intensity<=1)
        {
            _intensity += intensityChangeRate;
            yield return null;
        }
        _intensity = 1;
    }

    /*
    private IEnumerator SetFull(float duration)
    {
        yield return new WaitForSeconds(duration);
        _intensity = 0.99f;
    }*/

    public IEnumerator SceneStartEffect()
    {
        intensity = 1f;
        while(_intensity>0)
        {
            Debug.Log("intensity"+_intensity);
            _intensity -= intensityChangeRate;
            yield return null;
        }
        Debug.Log("intensity" + _intensity);
        _intensity = 0;
    }

    public void ScriptEffect()
    {
        ScriptEffectIn(valueChangeDurationIn);
        StartCoroutine(delayScriptEffectOut(valueChangeDurationIn));
    }

    IEnumerator delayScriptEffectOut(float duration)
    {
        yield return new WaitForSeconds(duration);
        ScriptEffectOut(valueChangeDurationOut);
    }

    public void ScriptEffectIn(float duration)
    {
        DG.Tweening.DOTween.To(() => brightnessAmount, x => brightnessAmount = x, targetValue, duration);
        DG.Tweening.DOTween.To(() => saturationAmount, x => saturationAmount = x, targetValue, duration);
        //DG.Tweening.DOTween.To(() => contrastAmount, x => contrastAmount = x, targetValue, duration);
    }
    public void ScriptEffectOut(float duration)
    {
        DG.Tweening.DOTween.To(() =>brightnessAmount, x => brightnessAmount = x, 1, duration);
        DG.Tweening.DOTween.To(() => saturationAmount, x => saturationAmount = x,1, duration);
       // DG.Tweening.DOTween.To(() => contrastAmount, x => contrastAmount = x,1, duration);
    }

    void Close()
    {
        isTrue = false;
        _intensity = 0;
        isReach = false;
        this.enabled = false;
    }
}
