using System;
using System.IO;
using Xamarin.Forms;
using AppPrevisaoDoTempo.Droid;

[assembly: Dependency(typeof(FileHelper))]

namespace AppPrevisaoDoTempo.Droid
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}