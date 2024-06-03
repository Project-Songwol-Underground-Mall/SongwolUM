using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;
    
    // �ʱ� ���� ������ �����ϱ� ���� ����
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

        Transform pointLightTransform = transform.Find("Point Light"); //TODO: ������Ʈ �̸��� (1) �پ� �ִµ� ����
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
        // ���� ���� ���� �ڷ�ƾ�� ������ �����մϴ�.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        // ������ ������ ���������� �����մϴ�.
        if (spotLight != null)
        {
            spotLight.color = Color.red;
        }
        if(pointLight != null)
        {
            pointLight.color = Color.red;
        }

        // ������ ���⸦ �ֱ������� �����ϴ� �ڷ�ƾ�� �����մϴ�.
        intensityCoroutine = StartCoroutine(PulseIntensity());
    }

    void ResetLight()
    {
        // ���� ���� ���� �ڷ�ƾ�� ������ �����մϴ�.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
            intensityCoroutine = null;
        }
        // spotLight & pointLight ���� ���� �� ���� ���� ���·� �ǵ����ϴ�.
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
            // ���⸦ ������ŵ�ϴ�.
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

            // ���⸦ ���ҽ�ŵ�ϴ�.
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
