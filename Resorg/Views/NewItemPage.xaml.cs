using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Resorg.Models;
using Resorg.Entities;
using Resorg.Services;

namespace Resorg.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public Resres Resource { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Subjects { get; set; } = new List<string>();
        public List<string> Fields { get; set; } = new List<string>();
        public string ResourceTags { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };
            Resource = new Resres
            {
                Title = "This is a Resource",
                Culture = new CultureInfo("en-US"),
                Uri = "https://localhost/resreses/resource.pdf"
            };

            Entries();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var db = new DbDataContext())
                {
                    List<string> categories = new List<string>();
                    categories.Add((string)CategoriesPicker.SelectedItem);
                    Resource.Categories = categories;
                    Resource.Subject = (string)SubjectsPicker.SelectedItem;
                    Resource.Field = (string)FieldsPicker.SelectedItem;
                    var tags = ResourceTags.Split(',');

                    foreach (string s in tags)
                    {
                        if (null == Resource.Tags)
                        {
                            Resource.Tags = new List<string>();
                        }
                        Resource.Tags.Add(s);
                    }

                    db.Add(Resource);
                    await db.SaveChangesAsync();
                }

                await DisplayAlert("Database", "Successfully strored resource", "OK");

                await Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Entries()
        {
            Categories.Add("Business and Management/Leadership");
            Categories.Add("Education");
            Categories.Add("Enterprise Management");
            Categories.Add("Humanities/Philosophy");

            Subjects.Add("Physics");
            Subjects.Add("Chemistry");
            Subjects.Add("Biology");
            Subjects.Add("Mathematics");

            Fields.Add("Electronics");
            Fields.Add("Mechanics");
            Fields.Add("Electrotechniques");
            Fields.Add("Computer Sciences");
        }
    }
}