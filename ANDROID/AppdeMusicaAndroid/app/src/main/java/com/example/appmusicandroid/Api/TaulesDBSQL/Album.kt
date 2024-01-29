package com.example.appmusicandroid.Api.TaulesDBSQL


import com.google.gson.annotations.SerializedName
import java.util.*

data class AlbumDBSQL(
    @SerializedName("AlbumName")
    val albumName: String? = null,

    @SerializedName("Year")
    val year: Int = 0,

    @SerializedName("SongUID")
    val songUID: UUID = UUID.randomUUID(),

    @SerializedName("Song")
    val song: SongDBSQL? = null
)