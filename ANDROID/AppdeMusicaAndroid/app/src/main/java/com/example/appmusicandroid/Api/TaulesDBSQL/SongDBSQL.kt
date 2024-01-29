package com.example.appmusicandroid.Api.TaulesDBSQL
import com.example.appmusicandroid.Api.SongDBSQL
import com.google.gson.annotations.SerializedName

import java.util.UUID;

data class SongDBSQL(
    @SerializedName("UID")
    val uid: UUID,

    @SerializedName("Title")
    val title: String?,

    @SerializedName("Language")
    val language: String?,

    @SerializedName("Duration")
    val duration: Int?,

    @SerializedName("VersionOriginalId")
    val versionOriginalId: UUID?,

    @SerializedName("OriginalSong")
    val originalSong: SongDBSQL?,

    @SerializedName("PlayObj")
    val playObj: PlayDBSQL?,

    @SerializedName("Songs")
    val songs: List<SongDBSQL> = ArrayList(),

    @SerializedName("Plays")
    val plays: List<PlayDBSQL> = ArrayList(),

    @SerializedName("Extensions")
    val extensions: List<ExtensionDBSQL> = ArrayList(),

    @SerializedName("PlayLists")
    val playLists: List<PlaylistDBSQL> = ArrayList(),

    @SerializedName("Albums")
    val albums: List<AlbumDBSQL> = ArrayList()
)