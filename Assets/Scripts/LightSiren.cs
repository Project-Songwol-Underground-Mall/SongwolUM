using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSiren : MonoBehaviour
{
    // Spotlight를 연결하기 위한 변수
    public Light spotLight;

    // 초기 조명 색상을 저장하기 위한 변수
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

        // 원래 색상과 세기를 저장합니다.
        originalColor = spotLight.color;
        originalIntensity = spotLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        // 'F' 키를 눌렀을 때 색상 변경 및 세기 변경 시작
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeToRedAndPulse();
        }

        // 'R' 키를 눌렀을 때 원래 상태로 되돌리기
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLight();
        }
    }

    void ChangeToRedAndPulse()
    {
        // 현재 실행 중인 코루틴이 있으면 중지합니다.
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        // 조명의 색상을 빨간색으로 변경합니다.
        spotLight.color = Color.red;

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

        // 조명의 색상과 세기를 원래 상태로 되돌립니다.
        spotLight.color = originalColor;
        spotLight.intensity = originalIntensity;
    }

    IEnumerator PulseIntensity()
    {
        while (true)
        {
            // 세기를 증가시킵니다.
            while (spotLight.intensity < 2f)
            {
                spotLight.intensity += Time.deltaTime;
                yield return null;
            }

            // 세기를 감소시킵니다.
            while (spotLight.intensity > 0.5f)
            {
                spotLight.intensity -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
