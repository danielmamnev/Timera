using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Timera.Views
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<Song>();

                var songs = conn.Table<Song>().ToList();
                songListView.ItemsSource = songs;
            }
        }

        bool cancellation;
        bool switchSong;

        string selectedSongName;
        float BPM;



        private void StartCounter(object sender, EventArgs e)
        {
            StartButton.IsVisible = false;
            StopButton.IsVisible = true;
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Click.mp3");

            

            

            cancellation = false;
            
            //BPM = 60000 / int.Parse(setBPM.Text); 
            
                Device.StartTimer(TimeSpan.FromMilliseconds(BPM), () =>
                {
                if (cancellation == false)
                {
                        player.Play();
                        if (bpmIndicator.BackgroundColor == Color.Black)
                        { bpmIndicator.BackgroundColor = Color.Blue; }
                        else
                        {
                            bpmIndicator.BackgroundColor = Color.Black;
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                });
            
        }

        private void StopCounter(object sender, EventArgs e)
        {
            StartButton.IsVisible = true;
            StopButton.IsVisible = false;
            cancellation = true;
          
        }

        void ToolbarItem_Activated(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NewSongPage());
        }

        void songListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            var selectedSong = (Song)e.SelectedItem;

            if(StopButton.IsVisible == true)
            {
                DisplayAlert("Alert", "Please stop timer before changing songs", "Go Back");
            }
            SelectedSongName.Text = selectedSong.Name;



            bpmIndicator.Text = selectedSong.BeatsPerMinute + " BPM";
            BPM = 60000 / selectedSong.BeatsPerMinute;

           
               
           
        
        

        }
    }
}
