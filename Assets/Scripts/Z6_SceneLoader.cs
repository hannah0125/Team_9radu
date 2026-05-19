using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위한 필수 네임스페이스

public class SimpleSceneLoader : MonoBehaviour
{
    // 💡 인스펙터 창에서 이동하고 싶은 다음 씬 이름을 자유롭게 적을 수 있는 칸
    [Header("이동할 목표 씬 이름")]
    public string targetSceneName;

    // 💡 VR 버튼이나 레이캐스트 이벤트에서 호출할 함수
    public void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            Debug.Log($"{targetSceneName} 씬으로 단순 이동합니다.");
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("목표 씬 이름(Target Scene Name)이 비어있습니다!");
        }
    }
}