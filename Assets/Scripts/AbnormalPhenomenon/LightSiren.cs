using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;
    public float StartDelay;
    public AudioClip sirenClip; // ���̷� ���带 ���� ����� �ҽ�

    public float enLightTime;
    public float deLightTime;

    private AudioSource audioSource; // ����� ����� ���� ����� �ҽ�

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

        audioSource = gameObject.AddComponent<AudioSource>();
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

        // ������ ���⸦ �ֱ������� �����ϴ� �ڷ�ƾ�� �����մϴ�.
        intensityCoroutine = StartCoroutine(PulseIntensity());
    }

    public void ResetLight()
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
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    IEnumerator PulseIntensity()
    {
        //��� ���� �ְ� �߻�
        yield return new WaitForSeconds(StartDelay);

        // ������ ������ ���������� �����մϴ�.
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
            // ���⸦ ������ŵ�ϴ�.
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

            // ���⸦ ���ҽ�ŵ�ϴ�.
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
