using System;
using KlipSDK.A2ASDK;
using KlipSDK.A2ASDK.Request;
using KlipSDK.A2ASDK.Response;
using UnityEngine;
using UnityEngine.UI;

public class ResultDemo : MonoBehaviour
{
    [SerializeField] private string ApplicationName = "Klip-A2A-SDK-Sample-03-Result";
    [SerializeField] private string AppCallbackSuccessUri = "klipdemo://request?success";
    [SerializeField] private string AppCallbackFailUri    = "klipdemo://request?fail";
    [SerializeField] private Text currentSetting;
    [SerializeField] private Text prepareAndRequestResultText;
    [SerializeField] private Text getResultText;
    
    public string requestKey = String.Empty;
    
    class AuthPrepareAndReqeustCallback : KlipCallback<KlipResponse>
    {
        private ResultDemo prepareDemo;
        
        public AuthPrepareAndReqeustCallback(ResultDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) {
            string resultKey = res.GetRequestKey();
            prepareDemo.prepareAndRequestResultText.text = string.Format($"onSuccess \nrequest key : {resultKey}\n");
            prepareDemo.requestKey = resultKey;
            
            Klip klip = new Klip();
            klip.Request( prepareDemo.requestKey );
        }

        public void onFail( KlipErrorResponse res) {
            prepareDemo.prepareAndRequestResultText.text = string.Format($"AuthPrepareCallback::onFail");
        }
    }
    
    class GetResultCallback : KlipCallback<KlipResponse>
    {
        private ResultDemo prepareDemo;
        
        public GetResultCallback(ResultDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) {
            var result = res.GetResult();
            var address = result.GetKlaytnAddress();
            
            prepareDemo.getResultText.text = string.Format($"KlaytnAddress : {address}\n");
        }

        public void onFail( KlipErrorResponse res) {
            prepareDemo.getResultText.text = string.Format($"GetResultCallback::onFail");
        }
    }
    
    void Start()
    {
        Application.deepLinkActivated += onDeepLinkActivated;
        currentSetting.text = string.Format($"ApplicationName : {ApplicationName}\nAppCallbackSuccessUri : {AppCallbackSuccessUri}\nAppCallbackFailUri : {AppCallbackFailUri}");
    }
    
    public void OnClickedPrepareRequestButton()
    {
        var app = new BAppInfo(ApplicationName);
        app.SetCallback(new BAppDeepLinkCB(AppCallbackSuccessUri, AppCallbackFailUri));
        Klip klip = new Klip();
        klip.Prepare( new AuthRequest(), app, new AuthPrepareAndReqeustCallback(this) );
    }
    
    public void OnClickedGetResultButton()
    {
        Klip klip = new Klip();
        klip.GetResult(requestKey, new GetResultCallback(this));
    }
    
    private void onDeepLinkActivated(string url)
    {
        prepareAndRequestResultText.text += "url :"+ url;
    }
}
