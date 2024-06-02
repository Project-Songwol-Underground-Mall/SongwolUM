using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlackout : MonoBehaviour
{
    public Light spotLight;
    public Light pointLight;

    private bool isSpotLightOn = true;
    private bool isPointLightOn = true;
    // Start is called before the first frame update
    void Start()
    {
        // 스포트라이트와 포인트라이트가 할당되지 않았다면, 현재 게임 오브젝트에서 Light 컴포넌트를 찾아 할당합니다.
        if (spotLight == null)
        {
            spotLight = GetComponent<Light>();
        }

        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 'S' 키를 눌렀을 때 스포트라이트를 켜고 끄기
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleSpotLight();
        }
        // 'P' 키를 눌렀을 때 포인트라이트를 켜고 끄기
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePointLight();
        }
    }
    // 스포트라이트의 상태를 토글하는 함수
    void ToggleSpotLight()
    {
        isSpotLightOn = !isSpotLightOn;
        spotLight.enabled = isSpotLightOn;
    }

    // 포인트라이트의 상태를 토글하는 함수
    void TogglePointLight()
    {
        isPointLightOn = !isPointLightOn;
        pointLight.enabled = isPointLightOn;
    }
}
