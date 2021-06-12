using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public List<Color> PlayerColors = new List<Color>();

    public RectTransform SelectionItem;
    public Transform ColorsHolder;
    bool IsPrimary = true;

    public Transform Player;
    public GameObject AskAdsScreen;

    int scnu = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i< ColorsHolder.childCount;i++)
        {
            Button gb = ColorsHolder.GetChild(i).GetChild(0).GetComponent<Button>();

            gb.image.color = PlayerColors[i];

            if (i > 1)
            {
                if (SaveAndLoad.IsColorUnlockedAt(i))
                {
                    ColorsHolder.GetChild(i).GetChild(1).gameObject.SetActive(false);
                }
            }
            //  gb.onClick.AddListener(() => { OnColorClick(i); });
        }
    }

    public void OnPrimaryClick(Button button)
    {
        SelectionItem.SetParent(button.transform);
        SelectionItem.anchoredPosition = Vector3.zero;
        IsPrimary = true;
        //_PrimaryColor

    }
    public void OnSecondaryClick(Button button)
    {
        SelectionItem.SetParent(button.transform);
        SelectionItem.anchoredPosition = Vector3.zero;
        IsPrimary = false;
        //_SecondaryColor
    }

    public void OnColorClick(int val)
    {
        foreach (Transform child in Player)
        {
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
            {
                if (IsPrimary)
                {
                    child.GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetColor("_PrimaryColor", PlayerColors[val]);
                    SaveAndLoad.SetPlayerPrimaryColor(PlayerColors[val]);
                    Debug.LogError("_PrimaryColor " + PlayerColors[val].GetHashCode());
                }
                else
                {
                    child.GetComponent<SkinnedMeshRenderer>().sharedMaterial.SetColor("_SecondaryColor", PlayerColors[val]);
                    SaveAndLoad.SetPlayerSecondaryColor(PlayerColors[val]);
                    Debug.LogError("_SecondaryColor " + PlayerColors[val].GetHashCode());
                }

            }
        }
    }
    public void OnLockBtnClick(int Val)
    {
        AskAdsScreen.SetActive(true);
        scnu = Val;
    }
    public void OnCloseBtnClick()
    {
        AskAdsScreen.SetActive(false);
        scnu = 0;
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }


    public void UnlockColorBy()
    {
        SaveAndLoad.SaveColorByPosition(scnu);
        AskAdsScreen.SetActive(false);
        Start();
    }


}
