using MusicPlayerLibrary.Crypto;

namespace Testing
{
    public class TestEncrypt
    {
        [SetUp]
        public void SetUp() { }

        [Test]
        public void EncryptPDFWithEncription()
        {
            string PDFruta = "CarpetaPDF\\Autovaloració.pdf";
            string PDFEncriptatRuta = "CarpetaPDF\\AutovaloracióEncrypted.pdf";
            string PDFDesencriptatRuta = "CarpetaPDF\\AutovaloracióDecrypted.pdf";
            string rutaCert = "CarpetaPDF\\certificat.pfx";
            string certPass = "123456";
            string publicKeyFile = "CarpetaPDF\\PublicKeyFile";

            RSACrypt.SavePublicKey(rutaCert,certPass, publicKeyFile);

            byte[] AESKey = Encryption.EncryptPDF(PDFruta, PDFEncriptatRuta, rutaCert, certPass, publicKeyFile);

            Encryption.DecryptPDF(PDFEncriptatRuta, PDFDesencriptatRuta, AESKey, rutaCert, certPass);

            byte[] rutaPDF = File.ReadAllBytes(PDFruta);
            byte[] rutaDecrypted = File.ReadAllBytes(PDFDesencriptatRuta);

            Assert.AreEqual(rutaDecrypted, rutaPDF);
        }
    }
}
