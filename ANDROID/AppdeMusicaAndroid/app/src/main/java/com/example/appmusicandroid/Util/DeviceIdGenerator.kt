package com.example.appmusicandroid.Util

import android.content.Context
import android.net.wifi.WifiInfo
import android.net.wifi.WifiManager
import android.os.Build
import android.provider.Settings
import java.security.MessageDigest
import java.util.*

object DeviceIdGenerator {

    // Método para obtener el identificador único del dispositivo
    fun getUniqueDeviceId(context: Context): String {
        val wifiManager = context.getSystemService(Context.WIFI_SERVICE) as WifiManager?

        // Intenta obtener la dirección MAC del dispositivo
        val macAddress = wifiManager?.connectionInfo?.macAddress

        // Si no se puede obtener la dirección MAC, utiliza el identificador del dispositivo
        return macAddress ?: getDeviceId(context)
    }

    // Método para obtener el identificador del dispositivo
    private fun getDeviceId(context: Context): String {
        val androidId = Settings.Secure.getString(context.contentResolver, Settings.Secure.ANDROID_ID)

        // Utiliza el identificador de Android si está disponible, de lo contrario, genera un UUID aleatorio
        return if (androidId != null && androidId != "9774d56d682e549c") {
            androidId
        } else {
            generateRandomUuid()
        }
    }

    // Método para generar un UUID aleatorio
    private fun generateRandomUuid(): String {
        return UUID.randomUUID().toString()
    }
}
