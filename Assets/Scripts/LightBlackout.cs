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
        // �ڽ� ������Ʈ �� �̸��� SpotLight�� ������Ʈ�� ã���ϴ�.
        Transform spotLightTransform = transform.Find("Spot Light");
        if (spotLightTransform != null)
        {
            spotLight = spotLightTransform.GetComponent<Light>();
        }
        else
        {
            Debug.LogError("SpotLight object not found on LightSystem object.");
        }

        // �ڽ� ������Ʈ �� �̸��� PointLight�� ������Ʈ�� ã���ϴ�.
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
        //TODO: �÷��̾� ��ġ�� ��� �������� �� ����

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
}