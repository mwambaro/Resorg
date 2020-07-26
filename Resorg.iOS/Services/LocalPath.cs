using System;
using System.IO;

using Xamarin.Forms;

using Resorg.Services;

[assembly: Dependency(typeof(Resorg.iOS.Services.LocalPath))]
namespace Resorg.iOS.Services
{
    public class LocalPath : ILocalPath
    {
        public string DatabasePath(string file)
        {
            string path = null;

            try
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", file);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return path;
        }
    }
}