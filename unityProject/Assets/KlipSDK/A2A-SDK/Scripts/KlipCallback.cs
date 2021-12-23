using KlipSDK.A2ASDK.Response;

namespace KlipSDK.A2ASDK
{
    public interface KlipCallback<T> where T : CallbackResponse{
        /**
         * Klip 요청 성공 Callback
         * @param result 성공 결과
         */
        void onSuccess(T result);

        /**
         * Klip 요청 실패 Callback
         * @param error 실패 결과
         */
        void onFail(KlipErrorResponse error);
    }
}