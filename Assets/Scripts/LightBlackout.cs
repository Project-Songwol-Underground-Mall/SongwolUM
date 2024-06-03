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
        // �ڽ� ������Ʈ �� �̸��� SpotLight�� ������Ʈ�� ã���ϴ�.
        Transform spotLightTransform = transform.Find("Spot Light");
        if (spotLightTransform != null)
        {
            spotLight = spotLightTransform.GetComponent<Light>();
        }

        // �ڽ� ������Ʈ �� �̸��� PointLight�� ������Ʈ�� ã���ϴ�.
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
        //TODO: �̻����� ��ȣ 9���� �� �۵��ǵ��� �ؾ� ��.
        //TODO: �÷��̾� ��ġ�� ��� �������� �� ����
        
    }

    IEnumerator BlinkLights()
    {
        // ������ �����̰� �մϴ�.
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

        // ������ ���ϴ�.
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }

        // ���� �ð� ���� ���� ���¸� �����մϴ�.
        yield return new WaitForSeconds(offDuration);

        // ������ �ٽ� �մϴ�.
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
        if (pointLight != null)
        {
            pointLight.enabled = true;
        }

    }
