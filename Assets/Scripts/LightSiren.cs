using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    // Spotlight�� �����ϱ� ���� ����
    public Light spotLight;

    // �ʱ� ���� ������ �����ϱ� ���� ����
    private Color originalColor;
    private float originalIntensity;
    private Coroutine intensityCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponent<Light>();
        }

        // ���� ����� ���⸦ �����մϴ�.
        originalColor = spotLight.color;
        originalIntensity = spotLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        // 'F' Ű�� ������ �� ���� ���� �� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeToRedAndPulse();
        }

        // 'R' Ű�� ������ �� ���� ���·� �ǵ�����
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLight();
        }
    }

    void ChangeToRedAndPulse()
    {
        // ���� ���� ���� �ڷ�ƾ�� ������ �����մϴ�.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        // ������ ������ ���������� �����մϴ�.
        spotLight.color = Color.red;

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

        // ������ ����� ���⸦ ���� ���·� �ǵ����ϴ�.
        spotLight.color = originalColor;
        spotLight.intensity = originalIntensity;
    }

    IEnumerator PulseIntensity()
    {
        while (true)
        {
            // ���⸦ ������ŵ�ϴ�.
            while (spotLight.intensity < 2f)
            {
                spotLight.intensity += Time.deltaTime;
                yield return null;
            }

            // ���⸦ ���ҽ�ŵ�ϴ�.
            while (spotLight.intensity > 0.5f)
            {
                spotLight.intensity -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
