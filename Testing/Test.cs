using MusicPlayerLibrary.Crypto;
using MusicalyAdminApp.API;
using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.TaulesTest;
using Newtonsoft.Json;

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

        [Test]
        public async Task CreateAndVerifyAPIAsync()
        {
            Apisql apisql = new Apisql();
            SongPost song = new SongPost
            {
                Title = "Hell is forever",
                Language = "English",
                Duration = 320,
            };
            string response;
            response = await apisql.PostSong(song);
            SongPost songObject = JsonConvert.DeserializeObject<SongPost>(response);
            Assert.AreEqual(song.Title, songObject.Title);
        }
    }
}