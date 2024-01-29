package com.example.appmusicandroid.Api.TaulesDBSQL

import com.google.gson.annotations.SerializedName

data class ExtensionDBSQL(
    @SerializedName("Name")
    val name: String? = null,

    @SerializedName("Songs")
    val songs: List<SongDBSQL> = ArrayList()
)