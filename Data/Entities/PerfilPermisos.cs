﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class PerfilPermisos
{
    public int Id { get; set; }

    public int IdPermiso { get; set; }

    public int IdPerfil { get; set; }

    public virtual Perfil IdPerfilNavigation { get; set; }

    public virtual Permisos IdPermisoNavigation { get; set; }
}