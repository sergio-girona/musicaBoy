package com.example.appmusicandroid.Util

import android.content.Context
import android.widget.Toast
import androidx.lifecycle.MutableLiveData
import com.example.appmusicandroid.Model.MusicModel
import com.example.appmusicandroid.Music.IFindMusic
import com.karumi.dexter.Dexter
import com.karumi.dexter.MultiplePermissionsReport
import com.karumi.dexter.PermissionToken
import com.karumi.dexter.listener.PermissionDeniedResponse
import com.karumi.dexter.listener.PermissionGrantedResponse
import com.karumi.dexter.listener.PermissionRequest
import com.karumi.dexter.listener.multi.MultiplePermissionsListener
import com.karumi.dexter.listener.single.PermissionListener

class CheckPermission(private val findMusic: IFindMusic) {

    val musicList = MutableLiveData<ArrayList<MusicModel>>()

    fun checkPermissions(context: Context) {
        Dexter.withContext(context)
            .withPermissions(
                android.Manifest.permission.READ_MEDIA_AUDIO,
                android.Manifest.permission.READ_EXTERNAL_STORAGE
            )
            .withListener(object : MultiplePermissionsListener {
                override fun onPermissionsChecked(report: MultiplePermissionsReport) {
                    if (report.areAllPermissionsGranted()) {
                        musicList.value = findMusic.getMusicFile(context)
                    } else {
                        // Verificar qué permisos fueron otorgados
                        val grantedPermissions = report.grantedPermissionResponses.map { it.permissionName }

                        if (grantedPermissions.contains(android.Manifest.permission.READ_MEDIA_AUDIO)) {
                            musicList.value = findMusic.getMusicFile(context)
                        }

                        if (grantedPermissions.contains(android.Manifest.permission.READ_EXTERNAL_STORAGE)) {
                            musicList.value = findMusic.getMusicFile(context)
                        }
                    }
                }

                override fun onPermissionRationaleShouldBeShown(
                    permissions: MutableList<PermissionRequest>?,
                    token: PermissionToken?
                ) {
                    token?.continuePermissionRequest()
                }
            }).check()
    }

}