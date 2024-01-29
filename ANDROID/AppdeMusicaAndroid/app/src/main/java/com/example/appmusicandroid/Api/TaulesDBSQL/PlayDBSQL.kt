package com.example.appmusicandroid.Api.TaulesDBSQL
import com.google.gson.annotations.SerializedName
import java.util.*

data class PlayDBSQL(
    @SerializedName("UIDSong")
    val uidSong: UUID,

    @SerializedName("Songs")
    val songs: List<SongDBSQL> = ArrayList(),

    @SerializedName("NameMusician")
    val nameMusician: String?,

    @SerializedName("Musicians")
    val musicians: List<MusicianDBSQL> = ArrayList(),

    @SerializedName("NameBand")
    val nameBand: String?,

    @SerializedName("Bands")
    val bands: List<BandDBSQL> = ArrayList(),

    @SerializedName("NameInstrument")
    val nameInstrument: String?,

    @SerializedName("Instruments")
    val instruments: List<InstrumentDBSQL> = ArrayList()
)