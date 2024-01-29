package com.example.appmusicandroid.Api.TaulesDBSQL

import com.example.appmusicandroid.Api.SongDBSQL
import com.google.gson.annotations.SerializedName
import java.time.LocalDateTime

data class PlaylistDBSQL(
    @SerializedName("Dispositiu")
    val dispositiu: String,

    @SerializedName("PlaylistName")
    val playlistName: String,

    @SerializedName("CreationDate")
    val creationDate: LocalDateTime?,

    @SerializedName("Songs")
    val songs: List<SongDBSQL> = ArrayList()
)