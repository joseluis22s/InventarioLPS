﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class TransaccionesEstadoItem
{
    public int Id { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public int IdEstatus { get; set; }

    public int IdItemInventario { get; set; }

    public virtual Estatus IdEstatusNavigation { get; set; }

    public virtual ItemInventario IdItemInventarioNavigation { get; set; }
}