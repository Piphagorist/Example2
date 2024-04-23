using System.Collections.Generic;
using SharpBase.DI;

namespace UnityBase.Connection
{
    public interface IServerConnectionParameterHolder : IShared
    {
        IReadOnlyDictionary<string, string> Parameters { get; }
    }
}