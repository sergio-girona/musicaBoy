package com.example.appmusicandroid.Api.TaulesDBSQL

import com.google.gson.annotations.SerializedName

data class InstrumentDBSQL(
    @SerializedName("Name")
    val name: String? = null,

    @SerializedName("Type")
    val type: String? = null,

    @SerializedName("Plays")
    val plays: List<PlayDBSQL> = ArrayList()
)