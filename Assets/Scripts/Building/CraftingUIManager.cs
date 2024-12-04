using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager instance {  get; private set; }

    [Header("UI References")]
    public GameObject craftingPanal;                //조합UI패널
    public TextMeshProUGUI buildingNameText;        //건물이름텍스트
    public Transform recipeContainer;               //레시피 버튼들이 들어갈 컨테이너
    public Button closeButton;                      //닫기 버튼
    public GameObject recipeButtonPerfabs;          //레시피버튼 프리팹

    private BuildingCrafter currentCrafter;         //현재 선택된 건물의 제작 시스템

    private void Awake()
    {
        if(instance == null) instance = this;       //싱글톤 설정
        else Destroy(gameObject);

        craftingPanal.SetActive(false);             //시작시 UI숨기기
    }


    private void RefreshRecipeList()       //레시피목록 새로고침
    {
        //기존 레시피 버튼들 제거
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        //새 레시피 버튼들 생성
        if (currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach (CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPerfabs, recipeContainer);
                RecipeButton recipeButton = buttonObj.GetComponent<RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
            }
        }
    }

    public void ShowUI(BuildingCrafter crafter)       //UI표시
    {
        currentCrafter = crafter;
        craftingPanal.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();
        }
    }

    public void HideUI()
    {
        craftingPanal.SetActive(false);
        currentCrafter = null;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}
