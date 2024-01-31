using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MusicPlayerLibrary.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerLibrary.GestioPDF
{
    /// <summary>
    /// Class used to Create and Add information to a PDF
    /// </summary>
    public class CreatePDF
    {
        /// <summary>
        /// This function allows you to create a new PDF
        /// </summary>
        /// <param name="rutaPDF"> Path for the PDF file (.../.../PDFfile.pdf)</param>
        /// <returns></returns>
        public static string Create(string rutaPDF)
        {
            try
            {
                using (PdfWriter writer = new PdfWriter(rutaPDF))
                using (PdfDocument pdf = new PdfDocument(writer))

                    return "Creat";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Error";
            }
        }
        /// <summary>
        /// Create a pdf based with a json
        /// </summary>
        /// <param name="rutaPdf">PDF path</param>
        /// <param name="jsonList">Json string that contains information</param>
        public static void CreatePDFWithJsonList(string rutaPdf, string jsonList)
        {
            try
            {
                dynamic jsonData = JsonConvert.DeserializeObject<ExpandoObject>(jsonList);

                // Create a PDF with the JSON data
                using (PdfWriter writer = new PdfWriter(rutaPdf))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(writer))
                    {
                        using (Document document = new Document(pdfDocument))
                        {
                            // Add each property of the JSON object to the PDF
                            foreach (var property in jsonData)
                            {
                                string propertyName = property.Key;
                                object propertyValue = property.Value;

                                if (propertyValue is List<object>)
                                {
                                    // If the property is a list, add each item as a list item
                                    document.Add(new Paragraph($"{propertyName}:"));
                                    var list = (List<object>)propertyValue;
                                    foreach (var item in list)
                                    {
                                        // If the item is a list, add each property as a list item
                                        var listItem = (ExpandoObject)item;
                                        foreach (var listItemProperty in listItem)
                                        {
                                            document.Add(new ListItem($"{listItemProperty.Key}: {listItemProperty.Value}"));
                                        }
                                        document.Add(new Paragraph(""));
                                        document.Add(new Paragraph(""));
                                    }
                                }
                                else
                                {
                                    // If the property is not a list, add it as a paragraph
                                    document.Add(new Paragraph($"{propertyName}: {propertyValue}"));
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("PDF Creat correctament");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al crear el PDF con la lista JSON: " + e.Message);
            }
        }

        /// <summary>
        /// Function used to create a pdf with info and sign it
        /// </summary>
        /// <param name="rutaOutputPDFSignado">Path where pdf wants to be save</param>
        /// <param name="jsonList"> string json that contains all json info</param>
        /// <param name="Certpass">Certificate password</param>
        /// <param name="CertificateRoute">Path to the certificate used to sign</param>
        public static void CrearPDFSignat(string rutaOutputPDFSignado, string jsonList,
            string Certpass, string CertificateRoute)
        {
            try
            {
                String rutaPDF = "ServerFitxers\\PDFsignat.pdf";
                CreatePDFWithJsonList(rutaPDF, jsonList);
                Sign sign = new Sign();
                sign.CreateSignPDF(CertificateRoute, Certpass, rutaOutputPDFSignado, rutaPDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}