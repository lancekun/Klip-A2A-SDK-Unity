using Newtonsoft.Json;

namespace KlipSDK.A2ASDK
{
    namespace Request
    {
        /**
         * 요청 BApp의 처리 결과 Callback 정보
         */
        public class BAppDeepLinkCB : JsonModelObject
        {
            [JsonProperty(KlipProtocol.BAPP_SUCCESS_URL)]
            public string successURL{ get; protected set; }
            [JsonProperty(KlipProtocol.BAPP_FAIL_URL)]
            public string failURL{ get; protected set; }

            public BAppDeepLinkCB(string successUrl, string failUrl)
            {
                this.successURL = successUrl;
                this.failURL    = failUrl;
            }
        }
        /**
         * 요청 BApp 정보
         */
        public class BAppInfo : JsonModelObject
        {
            [JsonProperty(KlipProtocol.BAPP_NAME)]
            public string name{ get; protected set; }
            [JsonProperty(KlipProtocol.BAPP_CALLBACK)]
            public BAppDeepLinkCB callback{ get; protected set; }

            public BAppInfo(string name)
            {
                this.name = name;
            }
            public void SetCallback(BAppDeepLinkCB callback) 
            {
                this.callback = callback;
            }
            public string GetName() 
            {
                return name;
            }

            public BAppDeepLinkCB GetCallback() 
            {
                return callback;
            }
        }
    }

    namespace Response
    {
        public class Card : JsonModelObject
        {
            [JsonProperty(KlipProtocol.CREATED_AT)]
            public int createdAt;
            [JsonProperty(KlipProtocol.UPDATED_AT)]
            public int updatedAt;
            [JsonProperty(KlipProtocol.OWNER)]
            public string owner;
            [JsonProperty(KlipProtocol.SENDER)]
            public string sender;
            [JsonProperty(KlipProtocol.CARD_ID)]
            public int cardId;
            [JsonProperty(KlipProtocol.CARD_URI)]
            public string cardUri;
            [JsonProperty(KlipProtocol.TRANSACTION_HASH)]
            public string transactionHash;
        }
        
        public class KlipError : JsonModelObject
        {
            [JsonProperty(KlipProtocol.CODE)]
            public int code;        // required
            [JsonProperty(KlipProtocol.MESSAGE)]
            public string message;  // required
        
        }
        
        /**
         * Klip 결과 정보
         */
        public class KlipResult : JsonModelObject
        {
            [JsonProperty(KlipProtocol.TX_HASH)]
            private string txHash; // exist if transaction type
            [JsonProperty(KlipProtocol.STATUS)]
            private string status;  // exist if transaction type
            [JsonProperty(KlipProtocol.KLAYTN_ADDRESS)]
            private string klaytnAddress; // exist if auth type
            
            /**
             * (Klip 트랜잭션 요청에 대한) 트랜잭션 해시를 가져온다.
             * @return 트랜잭션 해시
             */
            public string GetTxHash() {
                return txHash;
            }

            /**
             * (Klip 트랜잭션 요청에 대한) 상태 정보를 가져온다.
             * @return 상태 정보
             */
            public string GetStatus() {
                return status;
            }

            /**
             * (Klip 인증 요청에 대한) 사용자 EOA를 가져온다.
             * @return 사용자 EOA
             */
            public string GetKlaytnAddress() {
                return klaytnAddress;
            }
        }
    }
    
    public class JsonModelObject
    {
        public string ToJsonString()
        {
            // 예외 처리
            var result = JsonConvert.SerializeObject(this, Formatting.Indented);
            return result;
        }
    }
}