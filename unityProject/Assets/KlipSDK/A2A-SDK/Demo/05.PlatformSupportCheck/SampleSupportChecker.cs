using System;
using KlipSDK.A2ASDK;
using UnityEngine;

/**
 * SampleSupportChecker
 * KlipSupportChecker을 상속받아서 쓰는 샘플 코드입니다.
 */
public class SampleSupportChecker : KlipSupportChecker
{    
     /**
     * KakaoTalk 설치 여부를 확인한다.
     * @return KakaoTalk 설치 여부
     */
    public override bool isKakaoTalkInstalled()
     {
#if UNITY_ANDROID
         return isAndroidKakaoTalkInstalled();
#elif UNITY_IOS
         // iOS 네이티브 플러그인을 이용해서 카카오톡 설치 여부를 확인해야 합니다.
         return true; 
#endif
         return true;
     }
    
    /**
     * 설치된 KakaoTalk에서 Klip 지원 여부를 확인한다.
     * @return KakaoTalk의 Klip 지원 여부
     */
    public override bool isAvailable()
    {
        // 카카오톡 버전이 현재 Klip을 지원하는 버전인지 여부를 체크해야합니다.
        // KlipProtocol.KAKAO_KLIP_SUPPORT_VERSION 과 대조 필요
        return true;
    }

    bool isAndroidKakaoTalkInstalled()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;

        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", KlipProtocol.KAKAO_PACKAGE);
        }
        catch (Exception ex)
        {
            Debug.Log("exception" + ex.Message);
        }

        return (launchIntent == null ? false : true);
    }
    

}
