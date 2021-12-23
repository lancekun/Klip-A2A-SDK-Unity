using KlipSDK.A2ASDK.Response;
using Newtonsoft.Json;

namespace KlipSDK.A2ASDK
{
    namespace Request
    {
        public class KlipRequest : JsonModelObject
        {
            public const string AUTH = "auth";
            public const string SEND_KLAY = "send_klay";
            public const string SEND_TOKEN = "send_token";
            public const string SEND_CARD = "send_card";
            public const string EXECUTE_CONTRACT = "execute_contract";
        }
        
        /**
         * 인증 요청 정보
         */
        public class AuthRequest : KlipRequest
        {
            
        }
        
        /**
         * Card 전송 트랜잭션 요청 정보
         */
        public class CardTxRequest : KlipRequest
        {
            [JsonProperty(KlipProtocol.CONTRACT)]
            private string contract;
            [JsonProperty(KlipProtocol.TO)]
            private string to;
            [JsonProperty(KlipProtocol.CARD_ID)]
            private string cardId;
            [JsonProperty(KlipProtocol.FROM)]
            private string from;
        }
        
        /**
         * Contract 실행 트랜잭션 요청 정보
         */
        public class ContractTxRequest : KlipRequest 
        {
            [JsonProperty(KlipProtocol.TO)]
            private string to;
            [JsonProperty(KlipProtocol.VALUE)]
            private string value;
            [JsonProperty(KlipProtocol.ABI)]
            private string abi;
            [JsonProperty(KlipProtocol.PARAMS)]
            private string Params; // 수정 필요 
            [JsonProperty(KlipProtocol.FROM)]
            private string from;
        }
        
        /**
         * KLAY 전송 트랜잭션 요청 정보
         */
        public class KlayTxRequest : KlipRequest
        {
            [JsonProperty(KlipProtocol.TO)]
            private string to;
            [JsonProperty(KlipProtocol.AMOUNT)]
            private string amount;
            [JsonProperty(KlipProtocol.FROM)]
            private string from;
        }

        /**
         * Token 전송 트랜잭션 요청 정보
         */
        public class TokenTxRequest : KlipRequest
        {
            [JsonProperty(KlipProtocol.CONTRACT)]
            private string contract;
            [JsonProperty(KlipProtocol.TO)]
            private string to;
            [JsonProperty(KlipProtocol.AMOUNT)]
            private string amount;
            [JsonProperty(KlipProtocol.FROM)]
            private string from;
        }
    }
}