using System;
using KlipSDK.A2ASDK;
using KlipSDK.A2ASDK.Request;
using KlipSDK.A2ASDK.Response;
using UnityEngine;
using UnityEngine.UI;

public class RequestDemo : MonoBehaviour
{
    [SerializeField] private string ApplicationName = "Klip-A2A-SDK-Sample-02-Request";
    [SerializeField] private string AppCallbackSuccessUri = "klipdemo://request?success";
    [SerializeField] private string AppCallbackFailUri    = "klipdemo://request?fail";
    [SerializeField] private Text currentSetting;
    [SerializeField] private Text prepareResultText;
    [SerializeField] private Text requestResultText;
    
    public string requestKey = String.Empty;
    
    class AuthPrepareCallback : KlipCallback<KlipResponse>
    {
        private RequestDemo prepareDemo;
        
        public AuthPrepareCallback(RequestDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) {
            string status = res.GetStatus();
            string resultKey = res.GetRequestKey();
            string expirationTime = res.GetExpirationTime();

            prepareDemo.prepareResultText.text = string.Format($"onSuccess \nstatus : {status}\nrequest key : {resultKey} <- Request에 사용 예정\nexpirationTime : {expirationTime}");
            prepareDemo.requestKey = resultKey;
        }

        public void onFail( KlipErrorResponse res) {
            prepareDemo.prepareResultText.text = string.Format($"onFail");
        }
    }
    void Start()
    {
        Application.deepLinkActivated += onDeepLinkActivated;
        currentSetting.text = string.Format($"RequestDemo에 설정된 값\nApplicationName : {ApplicationName}\nAppCallbackSuccessUri : {AppCallbackSuccessUri}\nAppCallbackFailUri : {AppCallbackFailUri}");
    }
    
    public void OnClickedPrepareButton()
    {
        var app = new BAppInfo(ApplicationName);
        app.SetCallback(new BAppDeepLinkCB(AppCallbackSuccessUri, AppCallbackFailUri));
        Klip klip = new Klip();
        klip.Prepare( new AuthRequest(), app, new AuthPrepareCallback(this) );
    }
    
    public void OnClickedRequestButton()
    {
        Klip klip = new Klip();
        klip.Request( requestKey );
    }
    
    private void onDeepLinkActivated(string url)
    {
        string deeplinkURL = url;
        requestResultText.text = url;
    }
}
