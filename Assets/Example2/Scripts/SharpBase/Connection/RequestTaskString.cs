using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using SharpBase.Tasks;

namespace SharpBase.Connection
{
    public class RequestTaskString : TaskObject
    {
        private readonly Dictionary<string, string> _data;

        private readonly string _url;

        public RequestTaskString(string url, Dictionary<string, string> data)
        {
            _url = url;
            _data = data;
        }

        public string Result { get; protected set; }

        public override async void Start()
        {
            HttpContent content = new FormUrlEncodedContent(_data);
            HttpClient httpClient = new();
            HttpResponseMessage response = httpClient.PostAsync(_url, content).Result;
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader streamReader = new(stream);

            long? responseLength = response.Content.Headers.ContentLength;
            char[] buffer = new char[1024];
            StringBuilder sb = new();

            while (true)
            {
                int read = await streamReader.ReadAsync(buffer);
                if (read == 0) break;

                sb.Append(buffer, 0, read);
                Progress = (float) (sb.Length / (double) responseLength);
                InvokeUpdate();
            }

            SetResult(sb);

            content.Dispose();
            httpClient.Dispose();
            response.Dispose();
            await stream.DisposeAsync();
            streamReader.Dispose();
            
            InvokeComplete();
        }

        protected virtual void SetResult(StringBuilder stringBuilder)
        {
            Result = stringBuilder.ToString();
        }
    }
}