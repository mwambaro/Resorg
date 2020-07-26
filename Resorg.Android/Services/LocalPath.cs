using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;

using Resorg.Services;

[assembly: Dependency(typeof(Resorg.Droid.Services.LocalPath))]
namespace Resorg.Droid.Services
{
    public class LocalPath : ILocalPath
    {
        public string DatabasePath(string file)
        {
            string path = null;

            try
            {
                path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), file);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return path;
        }
    }
}