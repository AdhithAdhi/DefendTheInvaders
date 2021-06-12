using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpUi : MonoBehaviour
{
    protected Button button;
    public PowerUpType Type;
    protected int Count = 1;
    public Image FillImage;
    public TextMeshProUGUI CountText;
    public float timeLimit;
    protected float remainTime = 0f;
    protected bool claimed = false;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        Count = 1;
        CountText.text = Count + "x";
        remainTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (claimed)
        {
            remainTime -= Time.deltaTime;
            FillImage.fillAmount = remainTime / timeLimit;
        }

    }
    protected void ResetLoading()
    {
        //button = GetComponent<Button>();
        remainTime = timeLimit;
        FillImage.fillAmount = 1;
    }
    public void IncreaseCount()
    {
        Count++;
        CountText.text = Count + "x";
    }
    public void DecreseCount()
    {
        ResetLoading();
        Count--;
        if (Count > 0)
        {
            CountText.text = Count + "x";
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
