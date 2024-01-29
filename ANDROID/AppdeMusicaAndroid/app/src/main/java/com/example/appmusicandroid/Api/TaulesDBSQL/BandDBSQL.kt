package com.example.appmusicandroid.Api.TaulesDBSQL


import com.google.gson.annotations.SerializedName


data class BandDBSQL(
    @SerializedName("Name")
    val name: String? = null,

    @SerializedName("Origin")
    val origin: String? = null,

    @SerializedName("Genre")
    val genre: String? = null,

    @SerializedName("plays")
    val plays: List<PlayDBSQL> = ArrayList(),

    @SerializedName("Musicians")
    val musicians: List<MusicianDBSQL> = ArrayList()
)