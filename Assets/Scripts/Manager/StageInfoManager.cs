using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoManager : MonoBehaviour
{
    public TextMeshProUGUI TMPNumOfCorrectAnswer;
    public GameObject SafetyAlarmBoard;
    public GameObject StageInfoPanel;
    public GameObject PrevStageResultPanel;
    public GameObject NumOfCorrectAnswerPanel;

    // 스테이지 정보 패널 마테리얼 배열
    public Material[] StageInfoMaterial = new Material[GameManager.Instance.GetNumOfStages()];

    // 이전 스테이지 정답 여부 패널 마테리얼 배열
    public Material[] PrevStageResultMaterial = new Material[2]; 

    // 맞춘 정답 개수 패널 마테리얼 배열 -> VR실험 전용이크로 스크립트 옮길 필요
    public Material[] NumOfCorrectAnswerMaterial = new Material[16];

    void Start()
    {
        
    }

    public void ChangeStageInfoPanel()
    {
        int currentStage = GameManager.Instance.GetNumOfStages();
        if (currentStage == GameManager.Instance.GetCurrentStage())
        {
            StageInfoPanel.SetActive(false);
            return;
        }
        Renderer SIPRenderer = StageInfoPanel.GetComponent<Renderer>();
        if (SIPRenderer != null)
        {
            SIPRenderer.material = StageInfoMaterial[currentStage];
        }
    }

    public void ChangePSRPanel(bool IsCorrect)
    {
        int currentStage = GameManager.Instance.GetNumOfStages();
        if (currentStage == GameManager.Instance.GetCurrentStage())
        {
            PrevStageResultPanel.SetActive(false);
            return;
        }
        else
        {
            PrevStageResultPanel.SetActive(true);
        }

        Renderer SIPRenderer = PrevStageResultPanel.GetComponent<Renderer>();
        if (SIPRenderer != null)
        {
            if (IsCorrect) SIPRenderer.material = PrevStageResultMaterial[0];
            else SIPRenderer.material = PrevStageResultMaterial[1];
        }
    }
}
