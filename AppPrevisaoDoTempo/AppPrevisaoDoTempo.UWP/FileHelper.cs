using System.IO;
using Xamarin.Forms;
using AppPrevisaoDoTempo.UWP;
using Windows.Storage;

[assembly: Dependency(typeof(FileHelper))]
namespace AppPrevisaoDoTempo.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
