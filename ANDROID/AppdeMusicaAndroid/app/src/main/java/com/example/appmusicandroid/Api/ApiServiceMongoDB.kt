package com.example.appmusicandroid.Api

import android.content.ContentValues
import android.content.Context
import android.net.Uri
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
import com.example.appmusicandroid.Model.Audio
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.MultipartBody
import okhttp3.OkHttpClient
import okhttp3.RequestBody
import okhttp3.RequestBody.Companion.asRequestBody
import okhttp3.RequestBody.Companion.toRequestBody
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path
import java.io.File
import java.io.InputStream
import java.util.UUID
import java.util.concurrent.TimeUnit


interface SongApiService {
    @Multipart
    @POST("api/Audio")
    fun postSongAudio(
        @Part("Uid") songUid: String,
        @Part Audio: MultipartBody.Part
    ): Call<ResponseBody>
}

class ApiServiceSongMongoDB(private val context: Context) {

    private val IP_ADDRESS = "192.168.18.167:5180"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://$IP_ADDRESS/")
        .addConverterFactory(GsonConverterFactory.create())
        .client(OkHttpClient.Builder().connectTimeout(30, TimeUnit.SECONDS).build())
        .build()

    private val songApiService: SongApiService = retrofit.create(SongApiService::class.java)


    // Método para enviar un archivo de audio a la API
    fun postSongAudio(songUid: String, audioFile: File) {
        try {
            val requestFile = audioFile.asRequestBody("audio/*".toMediaTypeOrNull())
            val audioPart = MultipartBody.Part.createFormData("Audio", audioFile.name, requestFile)

            // Realiza la llamada a la API
            songApiService.postSongAudio(songUid, audioPart).enqueue(object : retrofit2.Callback<ResponseBody> {
                override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                    if (response.isSuccessful) {
                        Log.d("ApiServiceSongMongoDB", "Respuesta exitosa")
                        // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
                    } else {
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.code()}")
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.message()}")
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.body()}")
                    }
                }

                override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                    Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud onFailure: ${t.message}", t)
                }
            })
        } catch (e: Exception) {
            Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud: ${e.message}", e)
        }
    }
}