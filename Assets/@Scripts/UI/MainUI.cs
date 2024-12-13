using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text greenGemText;
    public TMP_Text redGemText;

    public BaseUI[] BaseUis;      // 관리할 BaseUI 배열
    public Button[] bottomButtons; // 하단 버튼 배열

    private int activeIndex = -1; // 현재 활성화된 UI 인덱스 (-1은 비활성 상태)

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        for (int i = 0; i < bottomButtons.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            bottomButtons[i].onClick.AddListener(() => OnBottomButtonClick(index));
        }

        // 자원 업데이트 이벤트 연결
        GameSystems.Data.OnGoldUpdated += OnHandleGoldChanged;
        GameSystems.Data.OnGreenGemUpdated += OnHandleGreenGemChanged;
        GameSystems.Data.OnRedGemUpdated += OnHandleRedGemChanged;

        foreach (var baseUi in BaseUis)
        {
            baseUi.CloseUI();
        }
    }

    public void OnHandleGoldChanged(int goldValue)
    {
        goldText.text = goldValue.ToString();
    }

    public void OnHandleGreenGemChanged(int greenGemValue)
    {
        greenGemText.text = greenGemValue.ToString();
    }

    public void OnHandleRedGemChanged(int redGemValue)
    {
        redGemText.text = redGemValue.ToString();
    }

    // 하단 버튼 클릭 시 UI 활성화/비활성화
    public void OnBottomButtonClick(int index)
    {
        if (activeIndex == index)
        {
            // 현재 활성화된 UI를 다시 클릭한 경우 비활성화
            BaseUis[index].CloseUI();
            activeIndex = -1;
        }
        else
        {
            // 모든 UI를 닫고 선택된 UI만 열기
            for (int i = 0; i < BaseUis.Length; i++)
            {
                if (i == index)
                {
                    BaseUis[i].OpenUI(); // 선택된 UI 열기
                }
                else
                {
                    BaseUis[i].CloseUI(); // 나머지 UI 닫기
                }
            }
            activeIndex = index; // 활성화된 UI의 인덱스 업데이트
        }
    }
}