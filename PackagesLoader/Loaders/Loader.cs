using System.IO;

namespace LoadingsManager.Loaders
{
    public abstract class Loader
    {
         public string ConfigFilePath { get; set; }

        public Loader(string configFilePath)
        {
            if(!this.FileCheck(configFilePath))
                throw new FileLoadException("File path hasn't passed control check. File has to exist and have extesnsion xml, check your path.");
            this.ConfigFilePath = configFilePath;
        }

        private bool FileCheck(string path)=>
            File.Exists(path) && Path.GetExtension(path) == ".xml";
    }
}