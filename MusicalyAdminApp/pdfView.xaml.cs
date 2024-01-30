using System.Windows;

namespace MusicalyAdminApp
{
    /// <summary>
    /// Interaction logic for pdfView.xaml
    /// </summary>
    public partial class pdfView : Window
    {
        public pdfView()
        {
            InitializeComponent();
            pdfWebViewer.Navigate(new Uri("about:blank"));

            // Load the PDF document.
            string fullPathToPDF = "C:\\Users\\jasma\\Dropbox\\PC\\Desktop\\TestdePdf.pdf";

            // Display the PDF document in the control.
            pdfWebViewer.Navigate(new Uri(fullPathToPDF));
        }
    }
}
