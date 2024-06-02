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
        // ����Ʈ����Ʈ�� ����Ʈ����Ʈ�� �Ҵ���� �ʾҴٸ�, ���� ���� ������Ʈ���� Light ������Ʈ�� ã�� �Ҵ��մϴ�.
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
        // 'S' Ű�� ������ �� ����Ʈ����Ʈ�� �Ѱ� ����
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleSpotLight();
        }
        // 'P' Ű�� ������ �� ����Ʈ����Ʈ�� �Ѱ� ����
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePointLight();
        }
    }
    // ����Ʈ����Ʈ�� ���¸� ����ϴ� �Լ�
    void ToggleSpotLight()
    {
        isSpotLightOn = !isSpotLightOn;
        spotLight.enabled = isSpotLightOn;
    }

    // ����Ʈ����Ʈ�� ���¸� ����ϴ� �Լ�
    void TogglePointLight()
    {
        isPointLightOn = !isPointLightOn;
        pointLight.enabled = isPointLightOn;
    }
}
