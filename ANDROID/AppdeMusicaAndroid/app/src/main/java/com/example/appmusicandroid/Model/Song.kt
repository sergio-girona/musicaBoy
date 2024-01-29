package com.example.appmusicandroid.Model

import com.google.gson.annotations.SerializedName

data class Song(
    val Title: String
)

data class Audio(
    @SerializedName("Id") val id: String?,
    @SerializedName("IdSql") val idSql: String,
    @SerializedName("Name") val name: String,
    @SerializedName("Content") val content: ByteArray?
)


