using System;
using System.IO;

using Xamarin.Forms;

using Resorg.Services;

[assembly: Dependency(typeof(Resorg.UWP.Services.LocalPath))]
namespace Resorg.UWP.Services
{
    public class LocalPath : ILocalPath
    {
        public string DatabasePath(string file)
        {
            string path = null;
            
            try
            { 
                path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, file);
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return path;
        }
    }
}
