package com.example.appmusicandroid.Api

import android.util.Log
import com.example.appmusicandroid.Api.TaulesDBSQL.SongDBSQL
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.POST
import java.util.UUID



interface SongApiServiceSQL {
    @POST("/api/Song")
    fun postSong(@Body song: SongDBSQL): Call<SongDBSQL>
}
class ApiServiceSongSQL {

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://192.168.18.167:5095/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()

    private val songApiServiceSQL: SongApiServiceSQL = retrofit.create(SongApiServiceSQL::class.java)

    // Método para convertir SongDBSQL a Son
    private fun generateNewUID(): String {
        return UUID.randomUUID().toString()
    }


    fun postSongSQL(songName: String): String {
        var responseJson: SongDBSQL? = null

        try {
            val myUid: String = generateNewUID().toString()
            val songPost = SongDBSQL(
                // Bind properties using apply
                // These are the default values or values you want to set
                uid,
                title,
                language,
                duration,
                versionOriginalId,
                originalSong,
                playObj,
                songs,
                plays,
                extensions,
                playLists,
                albums,
            )
            val response = songApiServiceSQL.postSong(songPost).execute()

            if (response.isSuccessful) {
                val responseBody = response.body()
                val defaultSongDBSQL = SongDBSQL(
                    uid = myUid,
                    title = "No té titul",
                    language = null,
                    duration = null,
                    versionOriginalId = null,
                    originalSong = null,
                    playObj = null,
                    songs = emptyList(),
                    plays = emptyList(),
                    extensions = emptyList(),
                    playLists = emptyList(),
                    albums = emptyList()
                )
                responseJson = responseBody ?: defaultSongDBSQL
                Log.d("ApiServiceSQL", "Respuesta exitosa: $responseBody")
                return myUid
            } else {
                Log.d("ApiServiceSQL", "Respuesta no exitosa: ${response.code()}")
            }
        } catch (e: Exception) {
            Log.e("ApiServiceSQL", "Error al realizar la solicitud: ${e.message}", e)
        }

        return "null"
    }
}
