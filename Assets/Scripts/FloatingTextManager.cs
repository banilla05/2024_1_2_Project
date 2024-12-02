using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;    //싱글톤
    public GameObject textPrefab;                  //UI 텍스트 프리팹

    private void Awake()                          //싱글톤등록
    {            
        instance = this;
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);                   //월드좌표를 스크린좌표로 변환

        GameObject textObj = Instantiate(textPrefab, transform);                        //UI텍스트 생성
        textObj.transform.position = screenPos;

        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();
        if (temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textObj));                                      //만든 애니메이션 효과 진행
        }
    }

    private IEnumerator AnimateText(GameObject textObj)                                  //폰트애니메이션 메서드
    {
        float duration = 1f;
        float timer = 0f;

        Vector3 startPos = textObj.transform.position;
        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();

        while (timer < duration)                                                           //타이머가 1초가 될 때까지
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textObj.transform.position = startPos + Vector3.up * (progress * 50f);         //폰트가 위로 올라가는 효과를 준다

            if (temp != null)                                                              //페이드 아웃효과
            {
                temp.alpha = 1 - progress;
            }

            yield return null;
        }

        Destroy(textObj);
    }
}
