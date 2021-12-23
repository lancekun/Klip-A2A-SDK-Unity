using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using KlipSDK.A2ASDK.Response;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace KlipSDK.A2ASDK
{
    public class NetworkService
    {
        public async Task Execute<T>(string requestURL, string requestMethod, string requestBody, KlipCallback<T> callback) where T : CallbackResponse
        {
            var result = await Execute(requestURL, requestMethod, requestBody);
            if (result != null)
            {
                try
                {
                    T response = JsonConvert.DeserializeObject<T>(result);
                    callback.onSuccess(response);
                }
                catch (Exception e)
                {
                    callback.onFail( new KlipErrorResponse(e));
                }
            }
            else
            {
                callback.onFail( new KlipErrorResponse(null) );
            }
        }
        
        public async Task<string> Execute(string requestURL, string requestMethod, string requestBody ) 
        {
            Uri uri = new Uri(requestURL);
            UnityWebRequest uwr = null;
            if (string.IsNullOrEmpty(requestBody))
            {
                uwr = UnityWebRequest.Get(uri);
            }
            else
            {
                uwr = UnityWebRequest.Put(uri,requestBody);
            }
            
            uwr.method = requestMethod;
            await uwr.SendWebRequest();
                
            if (uwr.error != null)
            {
                throw new KlipRequestException(KlipErrorCode.UNDEFINED_ERROR_CODE, uwr.error);
            }
                
            return uwr.downloadHandler.text;
        }
    }

    public class KlipResultTask<T> where T : CallbackResponse
    {
        private KlipCallback<T> callback;
        public KlipResultTask(KlipCallback<T> callback) 
        {
            this.callback = callback;
        }

        public T Call()
        {
            T result = null;
            Exception ex = null;

            return result;
        }

        public virtual void OnDidStart(){}
        public virtual void OnDidEnd(){}
    }
    
    public static class UnityWebRequestExtension
    {
        public static TaskAwaiter<UnityWebRequest.Result> GetAwaiter(this UnityWebRequestAsyncOperation reqOp)
        {
            TaskCompletionSource<UnityWebRequest.Result> tsc = new TaskCompletionSource<UnityWebRequest.Result>();
            reqOp.completed += asyncOp => tsc.TrySetResult(reqOp.webRequest.result);
 
            if (reqOp.isDone)
                tsc.TrySetResult(reqOp.webRequest.result);
 
            return tsc.Task.GetAwaiter();
        }
        
        public static async void WrapErrors(this Task task)
        {
            await task;
        }
    }
}
