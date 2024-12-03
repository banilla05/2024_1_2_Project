using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;         // 싱글톤
    public GameObject textPrefab;                       // UI 텍스트 프리팹

    private void Awake()
    {
        instance = this;                                // 싱글톤 등록
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject textOBJ = Instantiate(textPrefab, transform);
        textOBJ.transform.position = screenPos;

        TextMeshProUGUI temp = textOBJ.GetComponent<TextMeshProUGUI>();
        if(temp != null )
        {
            temp.text = text;

            StartCoroutine(AnimateText(textOBJ));
        }
    }

    private IEnumerator AnimateText(GameObject textOBJ)
    {
        float duration = 1f;                                                            // 동작 시간
        float timer = 0;                                                                // 사용할 타이머

        Vector3 startPos = textOBJ.transform.position;
        TextMeshProUGUI temp = textOBJ.GetComponent<TextMeshProUGUI>();                 // 받아온 오브젝트에서 TMP 폰트 참조

        while (timer < duration)                                                        // 타이머가 1초가 될때까지
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textOBJ.transform.position = startPos + Vector3.up * (progress * 50f);      // 폰트를 위로 올리는 효과를 준다

            if (temp != null )
            {
                temp.alpha = 1 - progress;
            }

            yield return null;
        }

        Destroy( textOBJ );
    }
}
