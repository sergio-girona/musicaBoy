package com.example.appmusicandroid.View

import android.content.ContentResolver
import android.content.Context
import android.content.Intent
import android.database.Cursor
import android.media.MediaPlayer
import android.net.Uri
import android.os.Bundle
import android.provider.MediaStore
import android.view.Gravity
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.example.appmusicandroid.R

class PlaylistSong : AppCompatActivity() {

    // Declaració de variables
    private lateinit var popupWindow: PopupWindow
    private lateinit var addSongImageView: ImageView
    private lateinit var playlistSongListView: ListView
    private lateinit var selectedSongsListView: ListView
    private var mediaPlayer: MediaPlayer? = null

    // Llista per emmagatzemar les cançons seleccionades
    private val selectedSongs = ArrayList<String>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_playlist_song)

        // Inicialització d'elements d'interfície d'usuari
        addSongImageView = findViewById(R.id.addSong)
        playlistSongListView = findViewById(R.id.playlistSongListView)


        val playlistName = intent.getStringExtra("playlistName")
        val playlistNameTextView: TextView = findViewById(R.id.playlistNameTextView)
        playlistNameTextView.text = "$playlistName"

        // Configuració de l'acció de clic al botó d'afegir cançons
        addSongImageView.setOnClickListener {
            showPopupWindow()
        }

        // Configuració de l'acció de clic al ListView de la llista de reproducció
        playlistSongListView.setOnItemClickListener { _, _, position, _ ->
            val selectedSong = selectedSongs[position]
            openMusicPlayerActivity(selectedSong)
        }
    }

    private fun openMusicPlayerActivity(selectedSong: String) {
        val intent = Intent(this, MusicPlayerActivity::class.java)
        intent.putExtra("selectedSong", selectedSong)
        startActivity(intent)
    }

    // Funció per mostrar la finestra emergent amb la llista de cançons
    private fun showPopupWindow() {
        val inflater: LayoutInflater = getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
        val view: View = inflater.inflate(R.layout.popup_song_list, null)

        val songListView: ListView = view.findViewById(R.id.songListView)
        val transferButton: Button = view.findViewById(R.id.transferButton)

        // Obtenció de la llista de cançons disponibles
        val songList = getSongList()

        // Configuració de l'adaptador del ListView amb la llista de cançons
        val adapter = ArrayAdapter(this, android.R.layout.simple_list_item_multiple_choice, songList)
        songListView.adapter = adapter
        songListView.choiceMode = ListView.CHOICE_MODE_MULTIPLE

        // Marcació de les cançons ja seleccionades
        for (song in selectedSongs) {
            val index = songList.indexOf(song)
            if (index != -1) {
                songListView.setItemChecked(index, true)
            }
        }

        // Acció de clic a les cançons de la llista emergent
        songListView.setOnItemClickListener { _, _, position, _ ->
            val selectedSong = songList[position]
            if (!selectedSongs.contains(selectedSong)) {
                selectedSongs.add(selectedSong)
            } else {
                selectedSongs.remove(selectedSong)
            }
        }

        // Acció de clic al botó de transferència
        transferButton.setOnClickListener {
            transferSelectedSongs()
            popupWindow.dismiss()
        }

        // Configuració de la finestra emergent i visualització
        popupWindow = PopupWindow(
            view,
            ViewGroup.LayoutParams.MATCH_PARENT,
            ViewGroup.LayoutParams.MATCH_PARENT,
            true
        )
        popupWindow.showAtLocation(view, Gravity.CENTER, 0, 0)
    }

    // Funció per obtenir la llista de cançons disponibles
    private fun getSongList(): ArrayList<String> {
        val songList = ArrayList<String>()
        val contentResolver: ContentResolver = contentResolver
        val uri: Uri = MediaStore.Audio.Media.EXTERNAL_CONTENT_URI
        val cursor: Cursor? = contentResolver.query(uri, null, null, null, null)

        cursor?.use {
            if (it.moveToFirst()) {
                val titleColumn: Int = it.getColumnIndex(MediaStore.Audio.Media.TITLE)

                do {
                    val title: String = it.getString(titleColumn)
                    songList.add(title)
                } while (it.moveToNext())
            } else {
                Toast.makeText(this, "No s'han trobat cançons.", Toast.LENGTH_SHORT).show()
            }
        }

        return songList
    }

    // Funció per transferir les cançons seleccionades a la llista de reproducció principal
    private fun transferSelectedSongs() {
        runOnUiThread {
            // Actualització del ListView principal amb les cançons seleccionades
            val adapter = ArrayAdapter(this, android.R.layout.simple_list_item_1, selectedSongs)
            playlistSongListView.adapter = adapter
        }
    }

    // Funció per reproduir una cançó
    private fun playSong(songTitle: String) {
        val songUri = getSongUri(songTitle)
        if (songUri != null) {
            mediaPlayer?.reset()
            mediaPlayer = MediaPlayer.create(this, songUri)
            mediaPlayer?.start()
        } else {
            Toast.makeText(this, "Error en obtenir la URI de la cançó.", Toast.LENGTH_SHORT).show()
        }
    }

    // Funció per obtenir la URI d'una cançó segons el títol
    private fun getSongUri(songTitle: String): Uri? {
        val contentResolver: ContentResolver = contentResolver
        val uri: Uri = MediaStore.Audio.Media.EXTERNAL_CONTENT_URI
        val cursor: Cursor? = contentResolver.query(uri, null, MediaStore.Audio.Media.TITLE + "=?", arrayOf(songTitle), null)

        cursor?.use {
            if (it.moveToFirst()) {
                val uriColumn: Int = it.getColumnIndex(MediaStore.Audio.Media.DATA)
                return Uri.parse(it.getString(uriColumn))
            }
        }

        return null
    }

    // Funció per alliberar recursos en tancar l'activitat
    override fun onDestroy() {
        super.onDestroy()
        mediaPlayer?.release()
    }
}