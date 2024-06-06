using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;
    public float StartDelay;
    public AudioClip sirenClip; // 사이렌 사운드를 위한 오디오 소스

    public float enLightTime;
    public float deLightTime;

    private AudioSource audioSource; // 오디오 재생을 위한 오디오 소스

    // 초기 조명 색상을 저장하기 위한 변수
    private Color originalSpotColor;
    private float originalSpotIntensity;

    private Color originalPointColor;
    private float originalPointIntensity;
    
    private Coroutine intensityCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Transform spotLightTransform = transform.Find("Spot Light");
        if (spotLightTransform != null )
        {
            spotLight = spotLightTransform.GetComponent<Light>();
            originalSpotColor = spotLight.color;
            originalSpotIntensity = spotLight.intensity;
        }

        Transform pointLightTransform = transform.Find("Point Light"); //TODO: 오브젝트 이름이 (1) 붙어 있는데 조정
        {
            pointLight = pointLightTransform.GetComponent<Light>();
            originalPointColor = pointLight.color;
            originalPointIntensity = pointLight.intensity;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToRedAndPulse()
    {
        // 현재 실행 중인 코루틴이 있으면 중지합니다.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        // 조명의 세기를 주기적으로 변경하는 코루틴을 시작합니다.
        intensityCoroutine = StartCoroutine(PulseIntensity());
    }

    public void ResetLight()
    {
        // 현재 실행 중인 코루틴이 있으면 중지합니다.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
            intensityCoroutine = null;
        }
        // spotLight & pointLight 조명 색상 및 세기 원래 상태로 되돌립니다.
        if(spotLight != null)
        {
            spotLight.color = originalSpotColor;
            spotLight.intensity = originalSpotIntensity;
        }
        if (pointLight!= null)
        {
            pointLight.color = originalPointColor;
            pointLight.intensity = originalPointIntensity;
        }
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    IEnumerator PulseIntensity()
    {
        //잠시 텀을 주고 발생
        yield return new WaitForSeconds(StartDelay);

        // 조명의 색상을 빨간색으로 변경합니다.
        if (spotLight != null)
        {
            spotLight.color = Color.red;
        }
        if (pointLight != null)
        {
            pointLight.color = Color.red;
        }

        if (audioSource != null && sirenClip != null)
        {
            audioSource.clip = sirenClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        while (true)
        {
            // 세기를 증가시킵니다.
            while ((spotLight != null && spotLight.intensity < 130f) ||
                   (pointLight != null && pointLight.intensity < 130f))
            {
                if (spotLight != null)
                {
                    spotLight.intensity += Time.deltaTime * enLightTime;
                }
                if (pointLight != null)
                {
                    pointLight.intensity += Time.deltaTime * enLightTime;
                }
                yield return null;
            }

            // 세기를 감소시킵니다.
            while ((spotLight != null && spotLight.intensity > 30f) ||
                   (pointLight != null && pointLight.intensity > 30f))
            {
                if (spotLight != null)
                {
                    spotLight.intensity -= Time.deltaTime * deLightTime;
                }
                if (pointLight != null)
                {
                    pointLight.intensity -= Time.deltaTime * deLightTime;
                }
                yield return null;
            }
        }
    }
}
