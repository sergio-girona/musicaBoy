package com.example.appmusicandroid.Api

import okhttp3.MultipartBody
import okhttp3.RequestBody
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path

interface ApiService {
    @GET("api/song")
    suspend fun getSongs(): Response<CloudMusicDataResponse>

    suspend fun getSongInformation(@Path("uid") songUid:String): Response<MusicItemResponse>

    @POST("api/Audio")
    suspend fun postSongAudio(@Body audio: ByteArray): Call<ResponseBody>
}
