using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using Resorg.Models;
using Resorg.Entities;

namespace Resorg.Services
{
    public class Dependency
    {
        public MockResresStore ResresStore => DependencyService.Get<IDataStore<Resres>>() as MockResresStore;
        public MockCategoryStore CategoryStore => DependencyService.Get<IDataStore<Category>>() as MockCategoryStore;
        public MockSubjectStore SubjectStore => DependencyService.Get<IDataStore<Subject>>() as MockSubjectStore;
        public MockFieldStore FieldStore => DependencyService.Get<IDataStore<Field>>() as MockFieldStore;
        public MockNoteStore NoteStore => DependencyService.Get<IDataStore<Note>>() as MockNoteStore;
        public MockTagStore TagStore => DependencyService.Get<IDataStore<Tag>>() as MockTagStore;

        /// <summary>
        /// Constructs db stores
        /// </summary>
        /// <param name="resresCmd">Only MockResresStore calls the others, so decide whether to init it from here.</param>
        public Dependency(bool resresCmd=true)
        {
            CategoryStore.MockCommand.Execute(null);
            SubjectStore.MockCommand.Execute(null);
            FieldStore.MockCommand.Execute(null);
            NoteStore.MockCommand.Execute(null);
            TagStore.MockCommand.Execute(null);

            if (resresCmd) ResresStore.MockCommand.Execute(null);
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

    }
}
