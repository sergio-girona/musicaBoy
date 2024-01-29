package com.example.appmusicandroid.Api

import com.google.gson.annotations.SerializedName
import okhttp3.MultipartBody

data class CloudMusicDataResponse(
    @SerializedName("\$values") val musicList: List<MusicItemResponse>
)

data class MusicItemResponse(
    @SerializedName("\$id") val id: String,
    @SerializedName("uid") val uid: String,
    @SerializedName("title") val title: String,
    @SerializedName("language") val language: String?,
    @SerializedName("duration") val duration: Int?,
    @SerializedName("versionOriginalId") val versionOriginalId: String?,
    @SerializedName("derivedVersions") val derivedVersions: DerivedVersions?,
    @SerializedName("originalSong") val originalSong: DerivedVersions?,  // Cambiado el nombre del campo y del tipo
    @SerializedName("playObj") val playObj: String?,
    @SerializedName("extensions") val extensions: String?,
    @SerializedName("playlists") val playlists: String?
)

data class DerivedVersions(
    @SerializedName("\$id") val id: String,
    @SerializedName("uid") val uid: String,
    @SerializedName("title") val title: String,
    @SerializedName("language") val language: String?,
    @SerializedName("duration") val duration: String?,
    @SerializedName("versionOriginalId") val versionOriginalId: String?,
    @SerializedName("originalSong") val originalSong: DerivedVersions?,  // Cambiado el nombre del campo y del tipo
    @SerializedName("playObj") val playObj: String?,
    @SerializedName("songs") val songs: Any?,
    @SerializedName("extensions") val extensions: String?,
    @SerializedName("playlists") val playlists: String?
)

data class Song(
    val uid: String?, // ID único de la canción
    val title: String?,
    val language: String?
)


