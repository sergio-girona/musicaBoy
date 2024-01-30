﻿using MusicPlayerLibrary.Certificates;
using MusicPlayerLibrary.Crypto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    /// <summary>
    /// Class Program of Client
    /// </summary>
    public class Program
    {
        static int PORT = 999;
        static TcpClient ActualClient;
        static String PDFRebut = "ClientFitxers\\PDFRebut.pdf";
        static String PDFDesencriptat = "ClientFitxers\\PDFRebutYDesencriptat.pdf";
        static String ClauPublicaRuta = "ClientFitxers\\PublicFileKey";
        static String RutaCertificado = "ClientFitxers\\certificat.pfx";
        static String CertPass = "123456";
        static String jsonRuta = "Config\\config_doc.json";
        static String IPServer = "";

        /// <summary>
        /// Main function of Client Program.cs
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                string jsonContent = File.ReadAllText(jsonRuta);
                dynamic configData = JObject.Parse(jsonContent);
                IPServer = configData.IP;

                var ipEndPoint = new IPEndPoint(IPAddress.Parse(IPServer), PORT);
                ActualClient = new TcpClient();
                ActualClient.Connect(ipEndPoint);
                Console.WriteLine("Conexió establida amb el servido");

                Console.WriteLine("Introdueix una lista per fer la consulta: ");
                string response = Console.ReadLine();

                // Crear certificat
                Autosigned.GeneratePfx(RutaCertificado, CertPass);

                RSACrypt.SavePublicKey(RutaCertificado, CertPass, ClauPublicaRuta);
                RSA rsa = RSACrypt.LoadPublicKey(ClauPublicaRuta);

                byte[] clauBytes = File.ReadAllBytes(ClauPublicaRuta);
                var clauBytesString = Convert.ToBase64String(clauBytes);
                // Exportar la clau pública en una cadena
                byte[] publicKeyBytes = rsa.ExportRSAPublicKey();
                String publicKeyString = Convert.ToBase64String(publicKeyBytes);

                // Ara pots utilitzar publicKeyString amb una cadena amb la teva resposta
                response = response + "|" + clauBytesString;
                SenderMessage(response);

                NetworkStream networkStream = ActualClient.GetStream();
                byte[] claveAesEncriptada = ReceiveMessage(networkStream);
                Receiver(PDFRebut, networkStream, claveAesEncriptada);
            }
            finally
            {
                if (ActualClient != null)
                {
                    ActualClient.Close();
                }
            }
        }
        /// <summary>
        /// Allows you to create a byte array using a NetworkStream.
        /// </summary>
        /// <param name="networkStream">Stream messege recived</param>
        /// <returns>A byte array with the stream message </returns>
        static public byte[] ReceiveMessage(NetworkStream networkStream)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[256];

                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recibir datos: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Reciver allows you decrypt, and save using a recived pdf document
        /// </summary>
        /// <param name="rutaDestino"></param>
        /// <param name="networkStream"></param>
        /// <param name="claveAesEncriptada"></param>
        static public void Receiver(string rutaDestino, NetworkStream networkStream, byte[] claveAesEncriptada)
        {
            try
            {
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];

                using (FileStream fileStream = File.Create(rutaDestino))
                {
                    int bytesRead;

                    
                    while ((bytesRead = networkStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                
                Encryption.DecryptPDF(PDFRebut, PDFDesencriptat, claveAesEncriptada, RutaCertificado, CertPass);
                
                networkStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar la respuesta del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Allows you send a message to the server
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static async Task SenderMessage(string message)
        {
            try
            {
                // Obtenir la referencia al fluxa de xarxa del client
                NetworkStream stream = ActualClient.GetStream();
                var EncMessage = Encoding.UTF8.GetBytes(message);
                StreamWriter writer = new StreamWriter(stream);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
                Console.WriteLine("Enviat: " + message);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }
    }
}