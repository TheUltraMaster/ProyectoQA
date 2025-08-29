
using Riok.Mapperly.Abstractions;
using BD.Data;
using BD.Models;
using Proyecto.DTOS;

namespace Proyecto.Mapper;

[Mapper]
public partial class Map
{
    public partial Area toEntity(AreaDto dto);
    
    public partial Empleado toEntity(EmpleadoDto dto);
    
    public partial Equipo toEntity(EquipoDto dto);
    
    public partial Marca toEntity(MarcaDto dto);
    
    public partial Reporte toEntity(ReporteDto dto);

    [MapProperty(nameof(UsuarioDto.Usuario), nameof(Usuario.Usuario1))]
    [MapProperty(nameof(UsuarioDto.Activo), nameof(Usuario.Activo), Use = nameof(MapBoolToUlong))]
    public partial Usuario toEntity(UsuarioDto dto);
    
    private ulong MapBoolToUlong(bool value) => value ? 1UL : 0UL;
    
    public partial Causa toEntity(CausaDto dto);
    
    public partial Vehiculo toEntity(VehiculoDto dto);
    
    public partial Imagen toEntity(ImagenDto dto);
    
    public partial Mobiliario toEntity(MobiliarioDto dto);
    
    public partial Herramientum toEntity(HerramientaDto dto);
    
    public partial Electronico toEntity(ElectronicoDto dto);
    
    public partial BitacoraEquipo toEntity(BitacoraEquipoDto dto);
}