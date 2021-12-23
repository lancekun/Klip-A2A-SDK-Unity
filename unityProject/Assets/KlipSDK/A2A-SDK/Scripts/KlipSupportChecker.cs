namespace KlipSDK.A2ASDK
{
    public class KlipSupportChecker
    {    
        /**
         * KakaoTalk 설치 여부를 확인한다.
         * @return KakaoTalk 설치 여부
         */
        public virtual bool isKakaoTalkInstalled()
        {
            return true;
        }
    
        /**
         * 설치된 KakaoTalk에서 Klip 지원 여부를 확인한다.
         * @return KakaoTalk의 Klip 지원 여부
         */
        public virtual bool isAvailable()
        {
            return true;
        }
    }
}