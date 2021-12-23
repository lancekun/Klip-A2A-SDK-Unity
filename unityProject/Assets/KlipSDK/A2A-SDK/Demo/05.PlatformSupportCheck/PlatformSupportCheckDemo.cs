using KlipSDK.A2ASDK;
using KlipSDK.A2ASDK.Request;
using KlipSDK.A2ASDK.Response;
using UnityEngine;
using UnityEngine.UI;

public class PlatformSupportCheckDemo : MonoBehaviour
{
    [SerializeField] private string ApplicationName = "Klip-A2A-SDK-Sample-05-PlatformSupportCheck";
    [SerializeField] private Text resultText;
    
    class AuthPrepareCallback : KlipCallback<KlipResponse>
    {
        private PlatformSupportCheckDemo prepareDemo;
        
        public AuthPrepareCallback(PlatformSupportCheckDemo demo)
        {
            prepareDemo = demo;
        }
        
        public void onSuccess( KlipResponse res) {
            string status = res.GetStatus();
            string resultKey = res.GetRequestKey();
            string expirationTime = res.GetExpirationTime();

            prepareDemo.resultText.text = string.Format($"onSuccess \nstatus : {status}\nrequest key : {resultKey} \nexpirationTime : {expirationTime}");
        }

        public void onFail( KlipErrorResponse res) {
            prepareDemo.resultText.text = string.Format($"onFail");
        }
    }
    
    public void OnClickedPrepareButton()
    {
        var app = new BAppInfo(ApplicationName);
        Klip klip = new Klip( new SampleSupportChecker() );
        klip.Prepare( new AuthRequest(), app, new AuthPrepareCallback(this) );
    }
    
}
