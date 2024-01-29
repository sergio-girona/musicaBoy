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

namespace MusicalyAdminApp
{
    public partial class SongInfo : UserControl
    {

        public SongInfo()
        {
            InitializeComponent();
        }

        public event EventHandler SaveClicked;

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NameInf.IsReadOnly = !NameInf.IsReadOnly;
            LangInf.IsReadOnly = !LangInf.IsReadOnly;
            DurationInf.IsReadOnly = !DurationInf.IsReadOnly;
            FormatInf.IsReadOnly = !FormatInf.IsReadOnly;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }

        public TextBox NomInfTextBox => NameInf;
        public TextBox IdiomaInfTextBox => LangInf;
        public TextBox DuracioInfTextBox => DurationInf;         
        public TextBox FormatInfTextBox => FormatInf;
    } 
}
