﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class DetalleVendido
{
    public int Id { get; set; }

    public int IdEstatusVendido { get; set; }

    public DateTime Fecha { get; set; }

    public int IdCliente { get; set; }

    public string Pozo { get; set; }

    public int IdGuiaRemision { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; }

    public virtual Estatus IdEstatusVendidoNavigation { get; set; }

    public virtual GuiaRemision IdGuiaRemisionNavigation { get; set; }
}