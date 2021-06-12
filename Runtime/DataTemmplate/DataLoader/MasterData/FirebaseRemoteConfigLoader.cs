
namespace RealbizGames.Data
{
    public class FirebaseRemoteConfigLoader : IMasterDataDictionaryLoader
    {
        public FirebaseRemoteConfigLoader(string key)
        {
            this.key = key;
        }

        public string key { get; private set; }
        public string load()
        {
            #if USING_DATA_FIREBASE_REMOTE_CONFIG
            string jsonString = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(key).StringValue;
            return jsonString;
            #else
            return string.Empty;
            #endif
        }
    }

}
