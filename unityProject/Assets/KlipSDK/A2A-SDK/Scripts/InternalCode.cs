using KlipSDK.A2ASDK.Request;
using Newtonsoft.Json;

namespace KlipSDK.A2ASDK
{
    public class KlipErrorCode 
    {
        /**
          * (Request Error) Klip SDK Internal 에러 (ex, Http 연결 실패)
          */
        public const  int CLIENT_INTERNAL_ERROR = 10;

        /**
          * (Request Error) Klip SDK 필수 파라미터가 누락된 경우 발생하는 에러
          */
        public const  int CORE_PARAMETER_MISSING = 11;

        /**
          * (Request Error) Klip SDK에서 지원하지 않는 요청 타입을 사용하는 경우 발생하는 에러
          */
        public const  int NOT_SUPPORTED_REQUEST_TYPE = 12;

        /**
          * (Response Error) Klip REST API 미지원 에러코드
          */
        public const  int UNDEFINED_ERROR_CODE = 21;

        /**
          * (Response Error) Klip Protocol 에러
          */
        public const int PROTOCOL_ERROR_CODE = 22;
    }
    
    class InternalRequest : JsonModelObject
    {
        [JsonProperty(KlipProtocol.REQUEST_KEY)]
        private string requestKey;
        public const string REQUEST_KEY = "request_key";

        public void SetRequestKey(string requestKey)
        {
            this.requestKey = requestKey;
        }

        public string GetRequestKey()
        {
            return requestKey;
        }
    }
    
    /**
     * Card 전송 트랜잭션 요청 정보
     */
    public class PrepareRequest : JsonModelObject
    {
        [JsonProperty(KlipProtocol.BAPP)]
        private BAppInfo bapp; // required
        [JsonProperty(KlipProtocol.TRANSACTION)]
        private KlipRequest transaction;
        [JsonProperty(KlipProtocol.TYPE)]
        private string type; // required

        public void SetBApp(BAppInfo bapp)
        {
          this.bapp = bapp;
        }
        public void SetTransaction(KlipRequest transaction) {
          this.transaction = transaction;
        }

        public void SetType(string type) {
          this.type = type;
        }

        public BAppInfo GetBapp() {
          return bapp;
        }

        public KlipRequest GetTransaction() {
          return transaction;
        }

        public string GetType() {
          return type;
        }
    }
}