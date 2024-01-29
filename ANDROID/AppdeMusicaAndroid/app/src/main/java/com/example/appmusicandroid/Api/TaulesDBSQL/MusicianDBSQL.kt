package com.example.appmusicandroid.Api.TaulesDBSQL

import com.google.gson.annotations.SerializedName
import java.util.*


data class MusicianDBSQL(
    @SerializedName("Name")
    val name: String?,
    @SerializedName("Age")
    val age: Int?,
    @SerializedName("Plays")
    val plays: List<PlayDBSQL> = ArrayList(),
    @SerializedName("Bands")
    val bands: List<BandDBSQL> = ArrayList()
)