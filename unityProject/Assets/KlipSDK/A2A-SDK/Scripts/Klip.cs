using KlipSDK.A2ASDK;
using KlipSDK.A2ASDK.Request;
using KlipSDK.A2ASDK.Response;
using UnityEngine;

public class Klip
{
    private KlipSupportChecker platformSupportChecker;
    private NetworkService networkService = new NetworkService();


    public Klip(KlipSupportChecker platformSupportChecker = null)
    {
        this.platformSupportChecker = platformSupportChecker;
        if (this.platformSupportChecker == null)
            this.platformSupportChecker = new KlipSupportChecker();
    }
    
    /**
     * Klip 요청 준비를 한다. (Request Key 획득)
     * @param request 요청하는 실행 정보
     * @param bappInfo 요청하는 BApp 정보
     * @param callback 요청 결과를 받는 Callback (이때, T값은 KlipResponse을 사용)
     * @throws KlipRequestException 입력 데이터가 유효하지 않을 경우 발생
     */
    public void Prepare<T>(KlipRequest request, BAppInfo bAppInfo, KlipCallback<T> callback) where T : CallbackResponse
    {
        if (request == null || bAppInfo == null || callback == null)
        {
            throw new KlipRequestException(KlipErrorCode.CORE_PARAMETER_MISSING, "required parameters is missing");
        }

        PrepareRequest req = new PrepareRequest();
        req.SetBApp(bAppInfo);

        if (request is AuthRequest)
        {
            req.SetType(KlipRequest.AUTH);
        }
        else if (request is KlayTxRequest)
        {
            req.SetType(KlipRequest.SEND_KLAY);
            req.SetTransaction(request);
        }
        else if (request is TokenTxRequest)
        {
            req.SetType(KlipRequest.SEND_TOKEN);
            req.SetTransaction(request);
        }
        else if (request is CardTxRequest)
        {
            req.SetType(KlipRequest.SEND_CARD);
            req.SetTransaction(request);
        }
        else if (request is ContractTxRequest)
        {
            req.SetType(KlipRequest.EXECUTE_CONTRACT);
            req.SetTransaction(request);
        }
        else
        {
            throw new KlipRequestException(KlipErrorCode.NOT_SUPPORTED_REQUEST_TYPE, "not supported request type");
        }
        
        networkService.Execute(getApiURL(KlipProtocol.API_PREPARE_URL), KlipProtocol.API_PREPARE_METHOD, req.ToJsonString(), callback).WrapErrors();
    }
    
    /**
     * Klip 요청 실행을 한다. (Deep Link 사용)
     * @param requestKey 요청 키
     * @throws KlipRequestException 입력 데이터가 유효하지 않을 경우 발생
     */
    public void Request(string requestKey)
    {
        string deelinkUrl;
        if (string.IsNullOrEmpty(requestKey))
        {
            throw new KlipRequestException(KlipErrorCode.CORE_PARAMETER_MISSING, "requestKey is required");
        }

        if (!platformSupportChecker.isKakaoTalkInstalled())
        {
            Debug.Log("kakaotalk is not installed. it's trying to open application market.");
#if UNITY_ANDROID
            deelinkUrl = "market://details?id=" + KlipProtocol.KAKAO_PACKAGE + "&referrer=" + KlipProtocol.INSTALL_REFERRER;
#elif UNITY_IOS || UNITY_IPHONE

#endif
        }
        else if(!platformSupportChecker.isAvailable())
        {
            Debug.Log("kakaotalk is not supported. it's trying to open application market to updated to the latest version.");
#if UNITY_ANDROID
            deelinkUrl = "market://details?id=" + KlipProtocol.KAKAO_PACKAGE + "&referrer=" + KlipProtocol.INSTALL_REFERRER;
#elif UNITY_IOS || UNITY_IPHONE

#endif
        }
        else
        {
            deelinkUrl ="kakaotalk://klipwallet/open?url=" + getLinkURL() + requestKey;
        }
        
#if UNITY_EDITOR
        Debug.LogWarning("Deep link doesn't work on Unity Editor. deelLinkUrl : " + deelinkUrl );
#endif   
        
#if UNITY_ANDROID
        OpenAndroidDeepLink(deelinkUrl);
#elif UNITY_IOS || UNITY_IPHONE
        Application.OpenURL(deelinkUrl);
#endif
    }
    
#if UNITY_ANDROID
    void OpenAndroidDeepLink(string deelinkUrl)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriData = uriClass.CallStatic<AndroidJavaObject>("parse", deelinkUrl);

        AndroidJavaObject i = new AndroidJavaObject("android.content.Intent");

        i.Call<AndroidJavaObject>("setAction", "android.intent.action.VIEW");
        i.Call<AndroidJavaObject>("setData", uriData);

        currentActivity.Call("startActivity", i);
    }
#endif
    
    
    /**
     * Klip 요청에 대한 실행 결과 데이터를 가져온다.
     * @param requestKey 요청 키
     * @param callback 요청 결과를 받는 Callback
     * @throws KlipRequestException 입력 데이터가 유효하지 않을 경우 발생
     */
    public void GetResult<T>(string requestKey, KlipCallback<T> callback) where T : CallbackResponse
    {
        if (string.IsNullOrEmpty(requestKey) || callback == null)
        {
            throw new KlipRequestException(KlipErrorCode.CORE_PARAMETER_MISSING, "required parameters is missing");
        }

        InternalRequest req = new InternalRequest();
        req.SetRequestKey(requestKey);

        string getParams = InternalRequest.REQUEST_KEY + "=" + req.GetRequestKey();
        networkService.Execute(getApiURL(KlipProtocol.API_RESULT_URL + "?" + getParams), KlipProtocol.API_RESULT_METHOD, null, callback).WrapErrors();
    }

    /**
     * 사용자가 보유한 카드 정보 목록을 가져온다.
     * @param cardAddress 조회하는 카드 주소
     * @param userAddress 조회하는 사용자 주소
     * @param cursor 조회하는 시작 커서
     * @param callback 요청 결과를 받는 Callback (이때, T값은 CardListResponse를 사용)
     * @throws KlipRequestException 입력 데이터가 유효하지 않을 경우 발생
     */
    public void GetCardList<T>(string cardAddress, string userAddress, string cursor, KlipCallback<T> callback) where T : CallbackResponse
    {
        if (string.IsNullOrEmpty(cardAddress) || string.IsNullOrEmpty(userAddress) || callback == null)
        {
            throw new KlipRequestException(KlipErrorCode.CORE_PARAMETER_MISSING, "required parameters is missing");
        }
        
        string getParams = "" + "sca=" + cardAddress + "&eoa=" + userAddress;
        if(!string.IsNullOrEmpty(cursor))
            getParams += "&cursor=" + cursor;
        
        networkService.Execute(getApiURL(KlipProtocol.API_GET_CARD_URL + "?" + getParams), KlipProtocol.API_GET_CARD_METHOD, null, callback).WrapErrors();
    }

    private string getApiURL(string path) 
    {
        return KlipProtocol.SCHEME + "://" + KlipProtocol.apiAuthority() + KlipProtocol.API_VERSION + path;
    }

    private string getLinkURL() 
    {
        return KlipProtocol.SCHEME + "://" + KlipProtocol.linkAuthority() + KlipProtocol.KAKAO_KLIP_LINK;
    }
    

}
