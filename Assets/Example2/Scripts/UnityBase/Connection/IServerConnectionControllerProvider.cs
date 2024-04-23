using System;
using System.Collections.Generic;
using SharpBase.Connection;
using SharpBase.DI;
using SharpBase.Tasks;

namespace UnityBase.Connection
{
    public interface IServerConnectionControllerProvider : IShared
    {
        RequestTaskString PostRequest(IPostData data);
    }
}