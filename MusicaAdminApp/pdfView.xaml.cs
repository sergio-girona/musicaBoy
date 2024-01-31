using System;
using System.Windows;
using System.Windows.Navigation;
using MusicPlayerLibrary.GestioPDF;

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

                // Suponiendo que fullPathToPDF es una variable disponible en esta clase
                string fullPathToPDF = "C:\\Users\\jasma\\Dropbox\\PC\\Desktop\\TestdePdf.pdf";
                pdfWebViewer.Navigate(new Uri(fullPathToPDF));
            }
        }
}
