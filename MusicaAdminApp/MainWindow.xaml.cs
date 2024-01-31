using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.Taules;

namespace MusicalyAdminApp
{
    /// <summary>
    /// Main class representing the main window of the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor of the MainWindow class. Initializes components and displays songs.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            apiSql = new Apisql();
            ShowSongs();

            WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Method to display all songs in the stack panel.
        /// </summary>
        /// <returns></returns>
        private async Task ShowSongs()
        {
            try
            {
                List<Song> songs = await apiSql.GetSongs();
                ListBoxCanciones.ItemsSource = songs;
                ListBoxCanciones.SelectionChanged += ListBoxCanciones_SelectionChanged;
                Inf.SaveClicked += SongInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting and displaying songs: {ex.Message}");
            }
        }

        /// <summary>
        /// Method that updates the information fields of a song with its current data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxCanciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Song selectedSong = ListBoxCanciones.SelectedItem as Song;
            if (selectedSong != null)
            {
                Inf.UIDInf.Text = $"{selectedSong.UID}";
                Inf.NomInfTextBox.Text = $"{selectedSong.Title}";
                Inf.IdiomaInfTextBox.Text = $"{selectedSong.Language}";
                Inf.DuracioInfTextBox.Text = $"{selectedSong.Duration}";
            }
        }

        /// <summary>
        /// This method is used to save the modified data in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SongInfo_SaveClicked(object sender, EventArgs e)
        {
            int Durationint;

            try
            {
                int.TryParse(Inf.DurationInf.Text, out Durationint);
                Song selectedSong = ListBoxCanciones.SelectedItem as Song;

                if (selectedSong != null)
                {
                    selectedSong.Title = Inf.NameInf.Text;
                    selectedSong.Language = Inf.LangInf.Text;
                    selectedSong.Duration = Durationint;
                    using (var apiSql = new Apisql())
                    {
                        Extension extensionSong = new Extension();
                        extensionSong.Name = Inf.FormatInf.Text;
                        selectedSong.Extensions = new List<Extension> { extensionSong };
                        string uidString = selectedSong.UID.ToString();
                        string updateResponse = await apiSql.UpdateSong(uidString, selectedSong);
                        Console.WriteLine(updateResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            apiSql.Dispose();
        }


        /// <summary>
        /// Button to search a song.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string uid = SrchBar.Text;
            if (uid == null)
            {
                ShowSongs();
            }
            else
            {
                List<Song> songs = await apiSql.GetSong(uid);
                ListBoxCanciones.ItemsSource = songs;
                ListBoxCanciones.SelectionChanged += ListBoxCanciones_SelectionChanged;
                Inf.SaveClicked += SongInfo_SaveClicked;
            }
        }

        /// <summary>
        /// Button to generate the pdf.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            pdfView pdfViewerWindow = new pdfView();
            pdfViewerWindow.Show();
        }
    }
}
