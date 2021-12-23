using System;

namespace KlipSDK.A2ASDK
{
    /**
     * Klip API 요청 관련 Exception Class
     */
    public class KlipRequestException : Exception
    {
        private int errorCode;
        private string errorMsg;

        public KlipRequestException(int errCode, string errorMsg)
        :base(errorMsg)
        {
            this.errorCode = errCode;
            this.errorMsg  = errorMsg;
        }
        public KlipRequestException(int errCode, Exception e)
            :base("KlipRequestException",e)
        {
            this.errorCode = errCode;
        }
        
        /**
         * 에러코드를 가져온다.
         * @return 에러코드
         */
        public int GetErrorCode()
        {
            return errorCode;
        }

        /**
         * 에러코드 설명을 가져온다.
         * @return 에러코드 설명
         */
        public string GetErrorMsg()
        {
            return errorMsg;
        }
    }
}