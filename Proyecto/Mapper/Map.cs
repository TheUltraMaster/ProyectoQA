
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
    
    [MapProperty(nameof(EquipoDto.TipoAlimentacion), nameof(Equipo.TipoAlimentacion), Use = nameof(MapTipoAlimentacionToString))]
    [MapProperty(nameof(EquipoDto.Estado), nameof(Equipo.Estado), Use = nameof(MapEstadoEquipoToString))]
    [MapProperty(nameof(EquipoDto.Tipo), nameof(Equipo.Tipo), Use = nameof(MapTipoEquipoToString))]
    public partial Equipo toEntity(EquipoDto dto);
    
    public partial Marca toEntity(MarcaDto dto);
    
    public partial Reporte toEntity(ReporteDto dto);

    [MapProperty(nameof(UsuarioDto.Usuario), nameof(Usuario.Usuario1))]
    [MapProperty(nameof(UsuarioDto.Activo), nameof(Usuario.Activo), Use = nameof(MapBoolToUlong))]
    public partial Usuario toEntity(UsuarioDto dto);
    
    private ulong MapBoolToUlong(bool value) => value ? 1UL : 0UL;
    
    public partial Causa DtoToCausa(CausaDto dto);
    
    public partial Vehiculo toEntity(VehiculoDto dto);
    
    public partial Imagen toEntity(ImagenDto dto);
    
    public partial Mobiliario toEntity(MobiliarioDto dto);
    
    public partial Herramientum toEntity(HerramientaDto dto);
    
    public partial Electronico toEntity(ElectronicoDto dto);
    
    public partial BitacoraEquipo toEntity(BitacoraEquipoDto dto);
    
    // Entity to DTO mappings
    public partial AreaDto toDto(Area entity);
    
    public partial EmpleadoDto toDto(Empleado entity);
    
    [MapProperty(nameof(Equipo.TipoAlimentacion), nameof(EquipoDto.TipoAlimentacion), Use = nameof(MapStringToTipoAlimentacion))]
    [MapProperty(nameof(Equipo.Estado), nameof(EquipoDto.Estado), Use = nameof(MapStringToEstadoEquipo))]
    [MapProperty(nameof(Equipo.Tipo), nameof(EquipoDto.Tipo), Use = nameof(MapStringToTipoEquipo))]
    [MapProperty(nameof(Equipo.IdMarcaNavigation.Nombre), nameof(EquipoDto.MarcaNombre))]
    public partial EquipoDto toDto(Equipo entity);
    
    public partial MarcaDto toDto(Marca entity);
    
    public partial ReporteDto toDto(Reporte entity);

    [MapProperty(nameof(Usuario.Usuario1), nameof(UsuarioDto.Usuario))]
    [MapProperty(nameof(Usuario.Activo), nameof(UsuarioDto.Activo), Use = nameof(MapUlongToBool))]
    public partial UsuarioDto toDto(Usuario entity);
    
    private bool MapUlongToBool(ulong value) => value != 0UL;
    
    public partial CausaDto CausaToDto(Causa entity);
    
    public partial VehiculoDto toDto(Vehiculo entity);
    
    public partial ImagenDto toDto(Imagen entity);
    
    public partial MobiliarioDto toDto(Mobiliario entity);
    
    public partial HerramientaDto toDto(Herramientum entity);
    
    public partial ElectronicoDto toDto(Electronico entity);
    
    public partial BitacoraEquipoDto toDto(BitacoraEquipo entity);
    
    // Enum mapping methods for Equipo
    private string? MapTipoAlimentacionToString(TipoAlimentacion? value) => value?.GetDescription();
    private string MapEstadoEquipoToString(EstadoEquipo value) => value.GetDescription();
    private string MapTipoEquipoToString(TipoEquipo value) => value.GetDescription();
    
    private TipoAlimentacion? MapStringToTipoAlimentacion(string? value) 
        => string.IsNullOrEmpty(value) ? null : EnumExtensions.GetEnumFromDescription<TipoAlimentacion>(value);
    private EstadoEquipo MapStringToEstadoEquipo(string? value) 
        => string.IsNullOrEmpty(value) ? EstadoEquipo.Activo : EnumExtensions.GetEnumFromDescription<EstadoEquipo>(value);
    private TipoEquipo MapStringToTipoEquipo(string value) 
        => EnumExtensions.GetEnumFromDescription<TipoEquipo>(value);

    // Custom mapping for EquipoDto to include employee name
    public EquipoDto ToDtoWithNames(Equipo equipo)
    {
        var dto = toDto(equipo);
        if (equipo.IdEmpleadoNavigation != null)
        {
            dto.EmpleadoNombre = $"{equipo.IdEmpleadoNavigation.PrimerNombre} {equipo.IdEmpleadoNavigation.PrimerApellido}";
        }
        return dto;
    }

    // Custom mapping for BitacoraEquipoDto to include related names
    public BitacoraEquipoDto ToDtoWithNames(BitacoraEquipo bitacoraEquipo)
    {
        var dto = toDto(bitacoraEquipo);
        return dto;
    }
}