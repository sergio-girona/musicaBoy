using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MusicPlayerLibrary.Crypto
{
    /// <summary>
    /// Class used to encrypt files
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// Encrypt a PDF file and save
        /// </summary>
        /// <param name="rutaPDF">PDF file that have to be encripted path</param>
        /// <param name="PDFEncriptado">Encripted PDF file path</param>
        /// <param name="certRute">Certificate pxf path</param>
        /// <param name="certPass">Password of Certificate</param>
        /// <param name="PublicKeyRoute">PublicKey file path</param>
        /// <returns>A byte array with encripted PDF</returns>
        public static byte[] EncryptPDF(string rutaPDF, string PDFEncriptado, string certRute, string certPass, string PublicKeyRoute)
        {
            try
            {
                // Obtenir la clau pública RSA des del certificat
                var pk = RSACrypt.LoadPublicKey(PublicKeyRoute).ExportParameters(false);

                // Encriptar la clau AES amb la clau pública RSA
                X509Certificate2 certificat = new X509Certificate2(certRute, certPass);

                // Llegir el contingut del arxiu PDF
                byte[] contingutPDF = File.ReadAllBytes(rutaPDF);
                byte[] clauAesEncriptada;

                // Encriptar el contingut del PDF amb AES
                using (FileStream fsInput = new FileStream(rutaPDF, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(PDFEncriptado, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {
                            aes.GenerateKey();
                            byte[] aesIV = new byte[16];
                            aes.IV = aesIV;
                            clauAesEncriptada = RSACrypt.EncryptAESKey(aes.Key, pk);
                            // Perform encryption
                            ICryptoTransform encryptor = aes.CreateEncryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }

                Console.WriteLine("Documento encriptado con éxito.");

                // Devolver les clau generades
                return clauAesEncriptada;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encriptar el PDF: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Functions used to decrypt a encrypted PDF
        /// </summary>
        /// <param name="rutaPDF">PDF file that have to be decrypted path</param>
        /// <param name="PDFDesencriptado">Encripted pdf path</param>
        /// <param name="claveAesEncriptada">AESKey encrypted</param>
        /// <param name="certRuta">Certificate pfx path</param>
        /// <param name="certPass">Certificate password</param>
        public static void DecryptPDF(string rutaPDF, string PDFDesencriptado, byte[] claveAesEncriptada, string certRuta, string certPass)
        {
            try
            {
                X509Certificate2 certificado = new X509Certificate2(certRuta, certPass);
                byte[] claveAesDesencriptada = RSACrypt.DecryptAESKeyWithPrivateKey(claveAesEncriptada, certificado);
                byte[] contenidoEncriptadoAES = File.ReadAllBytes(rutaPDF);

                using (FileStream fsInput = new FileStream(rutaPDF, FileMode.Open))
                {
                    using (FileStream fsOutput = new FileStream(PDFDesencriptado, FileMode.Create))
                    {
                        using (AesManaged aes = new AesManaged())
                        {

                            aes.Key = claveAesDesencriptada;
                            byte[] aesIV = new byte[16];
                            aes.IV = aesIV;


                            ICryptoTransform decryptor = aes.CreateDecryptor();
                            using (CryptoStream cs = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                            {
                                fsInput.CopyTo(cs);
                            }
                        }
                    }
                }

                Console.WriteLine("Documento desencriptado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desencriptar el PDF: {ex.Message}");
                throw;
            }
        }
    }
}
