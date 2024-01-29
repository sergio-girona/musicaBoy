package com.example.appmusicandroid.Api

import android.util.Log
import com.google.gson.annotations.SerializedName
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.POST
import java.util.UUID

data class SongDBSQL(
    @SerializedName("UID")
    val uid: String,

    @SerializedName("data")
    val date: String,

    @SerializedName("NomSong")
    val title: String,

    @SerializedName("Genere")
    val genre: String,

    @SerializedName("songs")
    var songs: MutableList<Song>?
)

interface SongApiServiceSQL {
    @POST("/api/Song")
    fun postSong(@Body song: SongPost): Call<SongDBSQL>
}

data class SongPost(
    val title: String,
    val uid: String
)

class ApiServiceSongSQL {

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://192.168.18.167:5095/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()

    private val songApiServiceSQL: SongApiServiceSQL = retrofit.create(SongApiServiceSQL::class.java)

    // Método para convertir SongDBSQL a Song
    private fun fromSongDBToSong(songDB: SongDBSQL): Song {
        return Song(
            songDB.uid,
            songDB.title,
            ""
        )
    }

    private fun generateNewUID(): String {
        return UUID.randomUUID().toString()
    }


    fun postSongSQL(songName: String): String {
        var responseJson: Song? = null

        try {
            var myUid = generateNewUID()
            val songPost = SongPost(songName, myUid)
            val response = songApiServiceSQL.postSong(songPost).execute()

            if (response.isSuccessful) {
                val responseBody = response.body()
                responseJson = fromSongDBToSong(responseBody ?: SongDBSQL(
                    uid = myUid,
                    date = "",
                    title = "No té titul",
                    genre = "",
                    songs = null
                ))
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
