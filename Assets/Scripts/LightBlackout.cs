using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlackout : MonoBehaviour
{
    public int PhenomenonNumber;

    public GameObject Player;

    public Light spotLight;
    public Light pointLight;

    public float blinkDuration = 1.0f;
    public float offDuration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 자식 오브젝트 중 이름이 SpotLight인 오브젝트를 찾습니다.
        Transform spotLightTransform = transform.Find("Spot Light");
        if (spotLightTransform != null)
        {
            spotLight = spotLightTransform.GetComponent<Light>();
        }

        // 자식 오브젝트 중 이름이 PointLight인 오브젝트를 찾습니다.
        Transform pointLightTransform = transform.Find("Point Light");
        if (pointLightTransform != null)
        {
            pointLight = pointLightTransform.GetComponent<Light>();
        }
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //TODO: 이상현상 번호 9번일 때 작동되도록 해야 함.
        //TODO: 플레이어 위치가 어디에 도착했을 때 정전
        
    }

    IEnumerator BlinkLights()
    {
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
