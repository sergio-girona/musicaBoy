package com.example.appmusicandroid.Api

import android.content.Context
import android.util.Log
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.POST
import java.util.concurrent.TimeUnit
import com.google.gson.annotations.SerializedName
import okhttp3.OkHttpClient

data class Historial(
    @SerializedName("Id")
    val Id: String?,

    @SerializedName("UidUser")
    val UidUser: String,

    @SerializedName("UidSong")
    val UidSong: String,

    @SerializedName("TitleSong")
    val TitleSong: String,
)

interface HistorialApiService {
    @POST("api/Historial")
    fun postHistorial(@Body historial: Historial): Call<Historial>
}

class ApiServiceHistorial(private val context: Context) {

    private val IP_ADDRESS = "192.168.18.167:5042"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://$IP_ADDRESS/")
        .addConverterFactory(GsonConverterFactory.create())
        .client(OkHttpClient.Builder().connectTimeout(30, TimeUnit.SECONDS).build())
        .build()

    private val historialApiService: HistorialApiService = retrofit.create(HistorialApiService::class.java)

    // Método para enviar un objeto Historial a la API
    fun postHistorial(historial: Historial) {
        try {
            // Realiza la llamada a la API
            historialApiService.postHistorial(historial).enqueue(object : Callback<Historial> {
                override fun onResponse(call: Call<Historial>, response: Response<Historial>) {
                    if (response.isSuccessful) {
                        Log.d("ApiServiceHistorial", "Respuesta exitosa")
                        // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
                    } else {
                        Log.d("ApiServiceHistorial", "Respuesta no exitosa: ${response.code()}")
                        Log.d("ApiServiceHistorial", "Respuesta no exitosa: ${response.message()}")
                        Log.d("ApiServiceHistorial", "Respuesta no exitosa: ${response.body()}")
                    }
                }

                override fun onFailure(call: Call<Historial>, t: Throwable) {
                    Log.e("ApiServiceHistorial", "Error al realizar la solicitud onFailure: ${t.message}", t)
                }
            })
        } catch (e: Exception) {
            Log.e("ApiServiceHistorial", "Error al realizar la solicitud: ${e.message}", e)
        }
    }
}
