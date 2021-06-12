using UnityEngine;

namespace RealbizGames.Data
{
    public class LocalFileLoader : IMasterDataDictionaryLoader
    {
        public string filePath { get; private set; }

        public LocalFileLoader(string filePath)
        {
            this.filePath = filePath;
        }

        public string load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(filePath);
            return textAsset.text;
        }
    }
}

