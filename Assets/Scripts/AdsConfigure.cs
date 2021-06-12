using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class AdsConfigure : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "3926286";
#elif UNITY_ANDROID
    private string gameId = "3926287";
#endif
    public List<AdButton> adButtons = new List<AdButton>();
    //public Button myButton;
    string mySurfacingId = "rewardedVideo";
    bool testMode = false;
    //public Adstype adstype;
    //public UnityEvent rewardEvent;
    string selectedBtn = "";
    void Start()
    {
        //if (myButton == null)
        //    return;
        StopCoroutine(ShowAdsWhenReady());
        StartCoroutine(ShowAdsWhenReady());
    }
    IEnumerator ShowAdsWhenReady()
    {
        Debug.LogError("Strted");
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        foreach (AdButton adBtn in adButtons)
        {

            adBtn.adButton.interactable = false;
            if (adBtn.adstype == Adstype.Reward)
            {
                mySurfacingId = "RetryReward";

                // Map the ShowRewardedVideo function to the button’s click listener:
                if (adBtn.adButton) adBtn.adButton.onClick.AddListener(() => ShowRewardedVideo(adBtn.buttonId, "RetryReward"));

            }
            else
            {
                mySurfacingId = "video";

                // Map the ShowRewardedVideo function to the button’s click listener:
                if (adBtn.adButton) adBtn.adButton.onClick.AddListener(()=>ShowInterstitialAd(adBtn.buttonId, "video"));

            }
        }
        while (!Advertisement.IsReady(mySurfacingId))
        {
            Debug.LogError("Waiting till "+ (!Advertisement.IsReady(mySurfacingId)).ToString());
            yield return null;
        }
        Debug.LogError("Completed");
        foreach (AdButton adBtn in adButtons)
        {
            // Set interactivity to be dependent on the Ad Unit or legacy Placement’s status:
            adBtn.adButton.interactable = Advertisement.IsReady(mySurfacingId);
        }
    }
    public void ShowInterstitialAd(string Id, string type)
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            selectedBtn = Id;
            Advertisement.Show(type);
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo(string Id,string type)
    {
        selectedBtn = Id;
        Advertisement.Show(type);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string surfacingId)
    {
        //if (myButton == null)
        //    return;

        //If the ready Ad Unit or legacy Placement is rewarded, activate the button:
        if (surfacingId == mySurfacingId)
        {
            //Debug.LogError("Ready");
            //myButton.interactable = true;

            StopCoroutine(ShowAdsWhenReady());
            StartCoroutine(ShowAdsWhenReady());
        }

    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            foreach(AdButton ad in adButtons)
            {

                if (selectedBtn.Equals(ad.buttonId))
                {
                    ad.rewardEvent.Invoke();
                    break;
                }
            }
            //rewardEvent.
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogError("The ad did not finish due to an error.");
        }

        //StartCoroutine(ShowAdsWhenReady());
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
public enum Adstype
{
    Reward,
    Normal,
}

[System.Serializable]
public class AdButton
{
    public string buttonId;
    public Adstype adstype;
    public Button adButton;
    public UnityEvent rewardEvent;

}
