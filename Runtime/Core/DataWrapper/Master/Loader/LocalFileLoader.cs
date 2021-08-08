using UnityEngine;

namespace RealbizGames.Data
{
    public class LocalFileLoader : IMasterDataDictionaryLoader
    {
        public const string TAG = "LocalFileLoader";
        
        public string filePath { get; private set; }

        public LocalFileLoader(string filePath)
        {
            this.filePath = filePath;
        }

        public string load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(filePath);
            // UnityEngine.Debug.LogFormat("{0} - {1}", TAG, textAsset.text);
            return textAsset.text;
        }
    }
}

