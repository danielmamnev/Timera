using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Timera.Views
{
    public partial class NewSongPage : ContentPage
    {
        public NewSongPage()
        {
            InitializeComponent();
        }

        void ToolbarItem_Activated(System.Object sender, System.EventArgs e)
        { 
            Song song = new Song()
            {
                Name = SongNameEntry.Text,
                BeatsPerMinute = int.Parse(BPMEntry.Text)
            };
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Song>();
                var numberOfRows = conn.Insert(song);

                if(numberOfRows > 0)
                {
                    DisplayAlert("Success", "Song Added", "Great");
                } else
                {
                    DisplayAlert("Failure", "Song Failed to be Added", "Try Again");
                }
                Navigation.PopAsync();
            }
        }


    }
}
