using System.Collections.Generic;
using SharpBase.DI;

namespace SharpBase.GlobalVariables
{
    public sealed class GlobalVariables : SharedObject
    {
        private Dictionary<VariableName, string> _stringVariables = new();

        public void SetString(VariableName name, string value)
        {
            _stringVariables[name] = value;
        }

        public string GetString(VariableName name)
        {
            _stringVariables.TryGetValue(name, out string result);

            return result;
        }
    }
}