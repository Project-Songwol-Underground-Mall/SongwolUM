using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlackout : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;

    public float blinkDuration = 1.0f;
    public float offDuration = 1.0f;

    private Coroutine blinkCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        // 자식 오브젝트 중 이름이 SpotLight인 오브젝트를 찾습니다.
        Transform spotLightTransform = transform.Find("Spot Light");
        if (spotLightTransform != null)
        {
            spotLight = spotLightTransform.GetComponent<Light>();
        }
        else
        {
            Debug.LogError("SpotLight object not found on LightSystem object.");
        }

        // 자식 오브젝트 중 이름이 PointLight인 오브젝트를 찾습니다.
        Transform pointLightTransform = transform.Find("Point Light");
        if (pointLightTransform != null)
        {
            pointLight = pointLightTransform.GetComponent<Light>();
        }
        else
        {
            Debug.LogError("PointLight object not found on LightSystem object.");
        }

    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //TODO: 플레이어 위치가 어디에 도착했을 때 정전

    }

    public void StartBlackout()
    {
        StartCoroutine(BlinkLights());
    }

    public void ResetLight()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
        if (pointLight != null)
        {
            pointLight.enabled = true;
        }
    }

    IEnumerator BlinkLights()
    {
        float StartDelay = Random.Range(3f, 7f);
        yield return new WaitForSeconds(StartDelay);

        // 조명을 깜빡이게 합니다.
        for (float i = 0; i < blinkDuration; i += 0.2f)
        {
            if (spotLight != null)
            {
                spotLight.enabled = !spotLight.enabled;
            }
            if (pointLight != null)
            {
                pointLight.enabled = !pointLight.enabled;
            }
            yield return new WaitForSeconds(0.2f);
        }

        // 조명을 끕니다.
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }

        // 일정 시간 동안 꺼진 상태를 유지합니다.
        yield return new WaitForSeconds(offDuration);

        // 조명을 다시 켭니다.
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
        if (pointLight != null)
        {
            pointLight.enabled = true;
        }

    }
}