using System;
using KlipSDK.A2ASDK;
using KlipSDK.A2ASDK.Request;
using KlipSDK.A2ASDK.Response;
using UnityEngine;
using UnityEngine.UI;

public class LinkKlipWalletDemo : MonoBehaviour
{
    [SerializeField] private string ApplicationName = "Klip-A2A-SDK-Sample-04-LinkKlipWallet";
    [SerializeField] private string AppCallbackSuccessUri = "klipdemo://request?success";
    [SerializeField] private string AppCallbackFailUri    = "klipdemo://request?fail";
    [SerializeField] private Text currentSetting;
    [SerializeField] private Text resultText;
    
    public string requestKey = String.Empty;
    
    class AuthPrepareAndReqeustCallback : KlipCallback<KlipResponse>
    {
        private LinkKlipWalletDemo prepareDemo;
        
        public AuthPrepareAndReqeustCallback(LinkKlipWalletDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) 
        {
            string resultKey = res.GetRequestKey();
            prepareDemo.requestKey = resultKey;
            
            Klip klip = new Klip();
            klip.Request( prepareDemo.requestKey );
        }

        public void onFail( KlipErrorResponse res) 
        {
            Debug.Log("AuthPrepareAndReqeustCallback::onFail");
        }
    }
    
    class GetResultCallback : KlipCallback<KlipResponse>
    {
        private LinkKlipWalletDemo prepareDemo;
        
        public GetResultCallback(LinkKlipWalletDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) {
            var result = res.GetResult();
            var address = result.GetKlaytnAddress();
            
            prepareDemo.resultText.text = string.Format($"KlaytnAddress : {address}\n");
        }

        public void onFail( KlipErrorResponse res) {
            Debug.Log("GetResultCallback::onFail");
        }
    }
    
    void Start()
    {
        Application.deepLinkActivated += onDeepLinkActivated;
        currentSetting.text = string.Format($"ApplicationName : {ApplicationName}\nAppCallbackSuccessUri : {AppCallbackSuccessUri}\nAppCallbackFailUri : {AppCallbackFailUri}");
    }
    
    public void OnClickedLinkKlipWalletButton()
    {
        var app = new BAppInfo(ApplicationName);
        app.SetCallback(new BAppDeepLinkCB(AppCallbackSuccessUri, AppCallbackFailUri));
        Klip klip = new Klip();
        klip.Prepare( new AuthRequest(), app, new AuthPrepareAndReqeustCallback(this) );
    }
    
    private void onDeepLinkActivated(string url)
    {
        Debug.Log("onDeepLinkActivated url :" + AppCallbackFailUri);
        if (url == AppCallbackSuccessUri)
        {
            Klip klip = new Klip();
            klip.GetResult(requestKey, new GetResultCallback(this));
        }
        else if(url == AppCallbackFailUri)
        {
            
        }
    }
}
