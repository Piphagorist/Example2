using UnityBase.Patterns;
using UnityEngine;

namespace UnityBase.Connection
{
    [CreateAssetMenu]
    public class ServerConnectionSettings : SingletonScriptableObject<ServerConnectionSettings>
    {
        [SerializeField] private string baseUrl;

        public string BaseUrl => baseUrl;
    }
}