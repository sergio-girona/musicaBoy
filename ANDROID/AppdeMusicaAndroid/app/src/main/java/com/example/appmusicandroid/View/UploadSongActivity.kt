package com.example.appmusicandroid.View

import android.annotation.SuppressLint
import android.app.Activity
import android.content.ContentResolver
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.provider.OpenableColumns
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AppCompatActivity
import androidx.documentfile.provider.DocumentFile
import com.example.appmusicandroid.Api.ApiService
import com.example.appmusicandroid.Api.ApiServiceSongMongoDB
import com.example.appmusicandroid.Api.ApiServiceSongSQL
import com.example.appmusicandroid.R
import com.example.appmusicandroid.databinding.ActivityUploadSongBinding
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.MultipartBody
import okhttp3.OkHttpClient
import okhttp3.RequestBody
import okhttp3.RequestBody.Companion.asRequestBody
import okhttp3.RequestBody.Companion.toRequestBody
import okhttp3.ResponseBody
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.File
import java.io.FileOutputStream
import java.io.IOException
import java.util.concurrent.TimeUnit

class UploadSongActivity : AppCompatActivity() {

    // Binding
    private lateinit var binding: ActivityUploadSongBinding

    // Constante para el selector de documentos
    private var audioFile: File? = null
    private val YOUR_REQUEST_CODE = 123 // Puedes cambiar esto según tus necesidades

    // API Service
    private val apiServiceSongSQL = ApiServiceSongSQL()
    private val apiServiceSongMongo = ApiServiceSongMongoDB(this)

    @SuppressLint("MissingInflatedId")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityUploadSongBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Configuración del botón "Seleccionar archivo de audio"
        val selectAudioButton: Button = findViewById(R.id.buttonSelectAudio)
        selectAudioButton.setOnClickListener {
            openAudioSelector()
        }

        // Configuración del botón "Enviar"
        val submitButton: Button = findViewById(R.id.buttonSubmit)
        submitButton.setOnClickListener {
            if (audioFile == null) {
                Toast.makeText(this, "Selecciona un archivo de audio", Toast.LENGTH_SHORT).show()
            } else if (binding.editTextSongName.text.isEmpty()) {
                Toast.makeText(this, "Ingresa el nombre de la canción", Toast.LENGTH_SHORT).show()
            }
            uploadSong()
        }
    }

    private fun openAudioSelector() {
        val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
        intent.addCategory(Intent.CATEGORY_OPENABLE)
        intent.type = "audio/*"
        startActivityForResult(intent, YOUR_REQUEST_CODE)
    }

    private fun uploadSong() {
        val songName = findViewById<EditText>(R.id.editTextSongName).text.toString()

        CoroutineScope(Dispatchers.IO).launch {
            try {
                audioFile?.let { file ->
                    val extensio = file.name.split(".").last()
                    Log.d("UploadActivity", "Extensión del archivo: $extensio")
                    val songUid = apiServiceSongSQL.postSongSQL(songName)
                    if (songUid != "null") {
                        apiServiceSongMongo.postSongAudio(songUid, file)
                    } else {
                        Log.e("UploadActivity", "Error: songUid nulo.")
                    }

                    runOnUiThread {
                        Log.d("UploadActivity", "Canción creada: $songName")
                        Toast.makeText(this@UploadSongActivity, "Canción creada", Toast.LENGTH_SHORT).show()
                    }
                } ?: run {
                    Log.e("UploadActivity", "Error: Archivo de audio nulo.")
                }
            } catch (e: Exception) {
                Log.e("UploadActivity", "Error al subir la canción: ${e.message}", e)
            }
        }
    }

    // Método para manejar el resultado del selector de documentos
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == YOUR_REQUEST_CODE && resultCode == Activity.RESULT_OK) {
            data?.data?.let { uri ->
                audioFile = getFileFromUri(uri)
            }
        }
    }

    // Función para obtener un File a partir de una URI
    private fun getFileFromUri(uri: Uri): File? {
        try {
            contentResolver.openInputStream(uri)?.use { inputStream ->
                val fileName = getFileName(uri)
                val file = File(filesDir, fileName)

                file.outputStream().use { outputStream ->
                    inputStream.copyTo(outputStream)
                }

                Log.d("UploadActivity", "Archivo seleccionado: ${file.name}")
                return file
            }
        } catch (e: Exception) {
            Log.e("UploadActivity", "Error al obtener el archivo: ${e.message}", e)
        }
        return null
    }

    @SuppressLint("Range")
    private fun getFileName(uri: Uri): String {
        val cursor = contentResolver.query(uri, null, null, null, null)
        cursor?.use {
            if (it.moveToFirst()) {
                val displayName = it.getString(it.getColumnIndex(OpenableColumns.DISPLAY_NAME))
                Log.d("UploadActivity", "Nombre del archivo: $displayName")
                return displayName ?: "unknown_file"
            }
        }
        // retorna un nombre de archivo por defecto
        return "file_${System.currentTimeMillis()}"
    }
}
