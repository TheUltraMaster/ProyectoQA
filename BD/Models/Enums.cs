using System.ComponentModel;

namespace BD.Models;

public enum TipoAlimentacion
{
    [Description("110v")]
    Voltaje110,
    
    [Description("220v")]
    Voltaje220,
    
    [Description("diesel")]
    Diesel,
    
    [Description("regular")]
    Regular,
    
    [Description("super")]
    Super,
    
    [Description("bateria")]
    Bateria,
    
    [Description("ninguna")]
    Ninguna
}

public enum EstadoEquipo
{
    [Description("activo")]
    Activo,
    
    [Description("inactivo")]
    Inactivo,
    
    [Description("mantenimiento")]
    Mantenimiento,
    
    [Description("suspendido")]
    Suspendido
}

public enum TipoEquipo
{
    [Description("mobiliario")]
    Mobiliario,
    
    [Description("vehiculo")]
    Vehiculo,
    
    [Description("electronico")]
    Electronico,
    
    [Description("herramienta")]
    Herramienta
}

public enum EstadoEmpleado
{
    [Description("activo")]
    Activo,
    
    [Description("inactivo")]
    Inactivo,
    
    [Description("vacaciones")]
    Vacaciones
}

public enum Conectividad
{
    [Description("bluetooth")]
    Bluetooth,
    
    [Description("wifi")]
    Wifi,
    
    [Description("gsm")]
    Gsm,
    
    [Description("nfc")]
    Nfc,
    
    [Description("bluetooth_wifi")]
    BluetoothWifi,
    
    [Description("bluetooth_gsm")]
    BluetoothGsm,
    
    [Description("bluetooth_nfc")]
    BluetoothNfc,
    
    [Description("wifi_gsm")]
    WifiGsm,
    
    [Description("wifi_nfc")]
    WifiNfc,
    
    [Description("gsm_nfc")]
    GsmNfc,
    
    [Description("bluetooth_wifi_gsm")]
    BluetoothWifiGsm,
    
    [Description("bluetooth_wifi_nfc")]
    BluetoothWifiNfc,
    
    [Description("bluetooth_gsm_nfc")]
    BluetoothGsmNfc,
    
    [Description("wifi_gsm_nfc")]
    WifiGsmNfc,
    
    [Description("bluetooth_wifi_gsm_nfc")]
    BluetoothWifiGsmNfc,
    
    [Description("ninguno")]
    Ninguno
}

public enum Operador
{
    [Description("starlink")]
    Starlink,
    
    [Description("claro")]
    Claro,
    
    [Description("tigo")]
    Tigo,
    
    [Description("comnet")]
    Comnet,
    
    [Description("verasat")]
    Verasat,
    
    [Description("telecom")]
    Telecom,
    
    [Description("ninguno")]
    Ninguno
}