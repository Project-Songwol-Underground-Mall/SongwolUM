using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoManager : MonoBehaviour
{
    public GameObject StageInfoPanel; // ���� �������� ��ȣ �г�
    public GameObject PrevStageResultPanel; // ���� �������� ���俩�� �г�
    public GameObject GameEndPanel; // ���� ���� �г�

    // �������� ���� �г� ���׸��� �迭
    public Material[] StageInfoMaterial = new Material[GameManager.Instance.GetNumOfStages()];

    // ���� �������� ���� ���� �г� ���׸��� �迭
    public Material[] PrevStageResultMaterial = new Material[2]; 

    // ���� ���� ���� �г� ���׸��� �迭 -> VR���� ������ũ�� ��ũ��Ʈ �ű� �ʿ�
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

    public void ChangePrevStageResultPanel(bool IsCorrect)
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

    public void SetAllPanelInactive()
    {
        StageInfoPanel.SetActive(false);
    }

    // VR Experiment Version������ ���
    private TextMeshProUGUI TMPNumOfCorrectAnswer;
    private GameObject SafetyAlarmBoard;
    private GameObject NumOfCorrectAnswerPanel;
    void ChangeNOCAPanel()
    {
        int currentStage = GameManager.Instance.GetNumOfStages();
        if (currentStage != 1) NumOfCorrectAnswerPanel.SetActive(true);

        if (currentStage == 18)
        {
            NumOfCorrectAnswerPanel.SetActive(false);
            return;
        }

        Renderer SIPRenderer = NumOfCorrectAnswerPanel.GetComponent<Renderer>();

        if (SIPRenderer != null)
        {
            // VR Experiment Version�� ���, ���� �Ŵ����� numOfCorrectAnswer ����
            // SIPRenderer.material = NumOfCorrectAnswerMaterial[GameManager.Instance.GetNumOfCorrectAnswer()];
        }
    }
}
