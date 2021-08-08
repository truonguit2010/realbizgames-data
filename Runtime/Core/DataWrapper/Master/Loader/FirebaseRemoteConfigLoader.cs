
namespace RealbizGames.Data
{
    public class FirebaseRemoteConfigLoader : IMasterDataDictionaryLoader
    {
        public const string TAG = "FirebaseRemoteConfigLoader";
        
        public FirebaseRemoteConfigLoader(string key)
        {
            this.key = key;
        }

        public string key { get; private set; }
        public string load()
        {
            #if USING_DATA_FIREBASE_REMOTE_CONFIG
            string jsonString = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
            UnityEngine.Debug.LogFormat("{0} - {1}", TAG, jsonString);
            return jsonString;
            #else
            return string.Empty;
            #endif
        }
    }

}
