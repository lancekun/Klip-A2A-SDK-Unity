using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KlipSDK.A2ASDK
{
    namespace Response
    {
        public class CallbackResponse
        {
            
        }
        
        public class KlipResponse : CallbackResponse
        {
            [JsonProperty(KlipProtocol.REQUEST_KEY)]
            private string requestKey; // required
            [JsonProperty(KlipProtocol.EXPIRATION_TIME)]
            private string expirationTime; // required
            [JsonProperty(KlipProtocol.STATUS)]
            private string status; // required
            [JsonProperty(KlipProtocol.RESULT)]
            private KlipResult result; // exist if success
            [JsonProperty(KlipProtocol.ERROR)]
            private KlipError error; // exist if error
            
            /**
             * Klip 요청 키를 가져온다.
             * @return Klip 요청 키
             */
            public string GetRequestKey() {
                return requestKey;
            }

            /**
             * Klip 요청 키 만료 시간을 가져온다.
             * @return Klip 요청 키 만료 시간
             */
            public string GetExpirationTime() {
                return expirationTime;
            }

            /**
             * Klip 요청 키 상태를 가져온다.
             * @return Klip 요청 키 상태
             */
            public string GetStatus() {
                return status;
            }

            /**
             * Klip 요청 성공시, 결과를 가져온다.
             * @return 결과 정보
             */
            public KlipResult GetResult() {
                return result;
            }

            /**
             * Klip 요청 실패시, 결과를 가져온다.
             * @return 에러 정보
             */
            public KlipError GetError() {
                return error;
            }
        }

        /**
         * Klip 에러
         */
        public class KlipErrorResponse 
        {
            private int errorCode;
            private string errorMsg;
            private int httpStatus;
            private Exception exception; // (optional) Klip 예외
            
            /**
             * 클라이언트 사이드에서 예외가 발생한 경우에 사용하는 생성자
             * @param e 클라이언트 사이드에서 발생한 Exception
             */
            public KlipErrorResponse(Exception e) {
                this.errorCode = KlipErrorCode.CLIENT_INTERNAL_ERROR;
                this.errorMsg = "klip client error";
//                this.httpStatus = HttpURLConnection.HTTP_INTERNAL_ERROR;
                this.exception = e;
            }
        }

        /**
         * Card 목록
         */
        public class CardListResponse : CallbackResponse
        {
            private string name;
            private string symbolImg;
            private List<Card> cards;
            private string nextCursor;
        }
    }
}