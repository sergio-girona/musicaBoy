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
            string rutaCert = "CarpetaPDF\\certificat.pfx";
            string certPass = "123456";

            //RSACrypt.SavePublicKey()

            //byte[] encriptat = Encryption.EncryptPDF();
        }
    }
}
