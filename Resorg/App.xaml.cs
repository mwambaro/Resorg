using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Resorg.Services;
using Resorg.Views;

namespace Resorg
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockResresStore>();
            DependencyService.Register<MockSubjectStore>();
            DependencyService.Register<MockFieldStore>();
            DependencyService.Register<MockNoteStore>();
            DependencyService.Register<MockTagStore>();
            DependencyService.Register<MockCategoryStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
