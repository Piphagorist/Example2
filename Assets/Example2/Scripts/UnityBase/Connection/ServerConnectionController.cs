using System.Collections.Generic;
using SharpBase.Connection;
using SharpBase.DI;

namespace UnityBase.Connection
{
    public class ServerConnectionController : SharedObject
    {
        protected ServerConnectionSettings _connectionSettings;

        public override void Init()
        {
            _connectionSettings = ServerConnectionSettings.Instance;
        }

        public virtual RequestTaskString GetRequestTask(IPostData data)
        {
            return null;
        }

        public RequestTaskString GetRequestTask(Dictionary<string, string> data)
        {
            return GetRequestTask(data, _connectionSettings.BaseUrl);
        }
        
        public RequestTaskString GetRequestTask(Dictionary<string, string> data, string url)
        {
            var newRequestTask = CreateRequestTask(data, url);
            Container.InjectAt(newRequestTask);
            
            return newRequestTask;
        }

        protected virtual RequestTaskString CreateRequestTask(Dictionary<string, string> data, string url)
        {
            return new RequestTaskString(url, data);
        }
    }
}