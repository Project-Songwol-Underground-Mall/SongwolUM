using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;
    
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

        // 조명의 색상을 빨간색으로 변경합니다.
        if (spotLight != null)
        {
            spotLight.color = Color.red;
        }
        if(pointLight != null)
        {
            pointLight.color = Color.red;
        }

        // 조명의 세기를 주기적으로 변경하는 코루틴을 시작합니다.
        intensityCoroutine = StartCoroutine(PulseIntensity());
    }

    void ResetLight()
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
    
    }

    IEnumerator PulseIntensity()
    {
        while (true)
        {
            // 세기를 증가시킵니다.
            while ((spotLight != null && spotLight.intensity < 2f) ||
                   (pointLight != null && pointLight.intensity < 2f))
            {
                if (spotLight != null)
                {
                    spotLight.intensity += Time.deltaTime;
                }
                if (pointLight != null)
                {
                    pointLight.intensity += Time.deltaTime;
                }
                yield return null;
            }

            // 세기를 감소시킵니다.
            while ((spotLight != null && spotLight.intensity > 0.5f) ||
                   (pointLight != null && pointLight.intensity > 0.5f))
            {
                if (spotLight != null)
                {
                    spotLight.intensity -= Time.deltaTime;
                }
                if (pointLight != null)
                {
                    pointLight.intensity -= Time.deltaTime;
                }
                yield return null;
            }
        }
    }
}
