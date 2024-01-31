using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminApp
{
    /// <summary>
    /// Lógica de interacción para SongInfo.xaml
    /// </summary>
    public partial class SongInfo : UserControl
    {
        /// <summary>
        /// Constructor of the SongInfo class.
        /// </summary>
        public SongInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered when the "Save" button is clicked.
        /// </summary>
        public event EventHandler SaveClicked;

        /// <summary>
        /// Toggle the read-only status of the textboxes when the "Edit" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NameInf.IsReadOnly = !NameInf.IsReadOnly;
            LangInf.IsReadOnly = !LangInf.IsReadOnly;
            DurationInf.IsReadOnly = !DurationInf.IsReadOnly;
            FormatInf.IsReadOnly = !FormatInf.IsReadOnly;
        }

        /// <summary>
        /// Trigger the SaveClicked event when the "Save" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Getters for accessing the textboxes.
        /// </summary>
        public TextBox NomInfTextBox => NameInf;
        public TextBox IdiomaInfTextBox => LangInf;
        public TextBox DuracioInfTextBox => DurationInf;
        public TextBox FormatInfTextBox => FormatInf;
    }
}
