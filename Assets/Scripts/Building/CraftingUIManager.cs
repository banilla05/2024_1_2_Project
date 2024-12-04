using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager instance {  get; private set; }

    [Header("UI References")]
    public GameObject craftingPanal;                   //����UI�г�
    public TextMeshProUGUI buildingNameText;           //�ǹ��̸��ؽ�Ʈ
    public Transform recipeContainer;                  //������ ��ư���� �� �����̳�
    public Button closeButton;                         //�ݱ� ��ư
    public GameObject recipeButtonPerfabs;             //�����ǹ�ư ������

    private BuildingCrafter currentCrafter;            //���� ���õ� �ǹ��� ���� �ý���

    private void Awake()
    {
        if(instance == null) instance = this;          //�̱��� ����
        else Destroy(gameObject);

        craftingPanal.SetActive(false);                //���۽� UI�����
    }


    private void RefreshRecipeList()                     //�����Ǹ�� ���ΰ�ħ
    {
        //���� ������ ��ư�� ����
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        //�� ������ ��ư�� ����
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

    public void ShowUI(BuildingCrafter crafter)       //UIǥ��
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
