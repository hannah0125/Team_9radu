using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionTimer : MonoBehaviour
{
    [Header("[설정] 제한 시간 (초 단위)")]
    public float limitTime = 60f;

    [Header("[연결] 시간을 표시할 TMP 텍스트")]
    public TextMeshProUGUI timerText;

    [Header("[연결] 시간이 끝났을 켜줄 실패 UI 오브젝트")]
    public GameObject failWindowUI;

    [Header("[연결] 처음에 꺼줄 미션 안내 UI 오브젝트")]
    public GameObject missionIntroUI; // 추가: 처음에 안내창을 꺼주기 위해 연결

    [Header("[연결] 미션 성공 시 켜줄 완료 UI 오브젝트")]
    public GameObject successWindowUI;

    private bool isTimerRunning = false; // 처음에는 false로 시작해서 멈춰둠!

    void Start()
    {
        isTimerRunning = false;
        UpdateTimerUI();

        // 시작할 때 성공, 실패 UI는 미리 꺼두기
        if (failWindowUI != null) failWindowUI.SetActive(false);
        if (successWindowUI != null) successWindowUI.SetActive(false);
    }

    void Update()
    {
        if (!isTimerRunning) return;

        limitTime -= Time.deltaTime;

        if (limitTime <= 0f)
        {
            limitTime = 0f;
            isTimerRunning = false;
            OnTimerExpired(); // 시간 종료 함수 호출
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"시간 제한: {Mathf.CeilToInt(limitTime)}초";
        }
    }

    public void StartMission()
    {
        isTimerRunning = true; // 1. 타이머 가동 시작!

        // 2. 미션 안내 글자 UI를 화면에서 안 보이게 끈다.
        if (missionIntroUI != null)
        {
            missionIntroUI.SetActive(false);
        }

        Debug.Log("미션 시작! 안내 UI 종료 및 타이머 가동.");
    }

    public void CompleteMission()
    {
        if (!isTimerRunning) return; // 이미 타이머가 멈췄다면 실행 안 함

        isTimerRunning = false; // 1. 타이머를 즉시 멈춘다!

        // 2. 타이머 글자 자체를 화면에서 완전히 사라지게 숨긴다.
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        // 3. 숨겨놨던 미션 완료 UI 창을 짠! 하고 켜준다.
        if (successWindowUI != null)
        {
            successWindowUI.SetActive(true);
        }

        Debug.Log("미션 성공! 타이머가 정지 및 은닉되었고 완료 UI가 활성화되었습니다.");
    }

    // 0초가 되었을 때 실행되는 함수
    void OnTimerExpired()
    {
        Debug.Log("시간 초과! 실패 UI 창을 활성화합니다.");

        // 자동으로 워프하는 대신, 숨겨놨던 실패 UI 창을 짠! 하고 켜준다.
        if (failWindowUI != null)
        {
            failWindowUI.SetActive(true);
        }
    }
}