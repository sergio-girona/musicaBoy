package com.example.appmusicandroid.View

import android.app.AlertDialog
import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.EditText
import android.widget.LinearLayout
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.appmusicandroid.R

class Playlist : AppCompatActivity() {

    private val playlists = mutableListOf(
        Playlist("Favorites", "Tus canciones preferidas")
    )

    private lateinit var adapter: PlaylistAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_playlist)

        val recyclerView: RecyclerView = findViewById(R.id.recyclerViewPlaylists)
        adapter = PlaylistAdapter(playlists)

        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = adapter

        val linearLayout: LinearLayout = findViewById(R.id.playlistHeaderLayout)

        linearLayout.setOnClickListener {
            showAddPlaylistDialog()
        }
    }

    private fun showAddPlaylistDialog() {
        val builder = AlertDialog.Builder(this)
        builder.setTitle("Add Playlist")

        val view = LayoutInflater.from(this).inflate(R.layout.dialog_add_playlist, null)
        builder.setView(view)

        val editTextName: EditText = view.findViewById(R.id.editTextName)
        val editTextArtist: EditText = view.findViewById(R.id.editTextArtist)

        builder.setPositiveButton("Agregar") { dialog, which ->
            val name = editTextName.text.toString()
            val artist = editTextArtist.text.toString()

            if (name.isNotEmpty() && artist.isNotEmpty()) {
                val newPlaylist = Playlist(name, artist)
                playlists.add(newPlaylist)
                adapter.notifyItemInserted(playlists.size - 1)
            } else {
                Toast.makeText(this, "Ingrese tanto nombre de artista como una descripcion", Toast.LENGTH_SHORT).show()
            }
        }

        builder.setNegativeButton("Cancelar") { dialog, which ->
            dialog.dismiss()
        }
        val dialog = builder.create()
        dialog.show()
    }

    data class Playlist(val name: String, val artist: String)

    inner class PlaylistAdapter(private val playlists: List<Playlist>) : RecyclerView.Adapter<PlaylistAdapter.PlaylistViewHolder>() {

        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlaylistViewHolder {
            val view = LayoutInflater.from(parent.context).inflate(R.layout.current_playlist_row, parent, false)
            return PlaylistViewHolder(view)
        }

        override fun onBindViewHolder(holder: PlaylistViewHolder, position: Int) {
            val playlist = playlists[position]

            holder.textName.text = playlist.name
            holder.artistName.text = playlist.artist

            holder.itemView.setOnClickListener {
                val intent = Intent(holder.itemView.context, PlaylistSong::class.java)
                intent.putExtra("playlistName", playlist.name)
                holder.itemView.context.startActivity(intent)
            }
        }

        override fun getItemCount(): Int {
            return playlists.size
        }

        inner class PlaylistViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
            val textName: TextView = itemView.findViewById(R.id.textName)
            val artistName: TextView = itemView.findViewById(R.id.artistName)
        }
    }
}