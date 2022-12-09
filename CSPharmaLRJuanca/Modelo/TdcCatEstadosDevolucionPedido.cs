﻿using System;
using System.Collections.Generic;

namespace CSPharmaLRJuanca.Modelo;

public partial class TdcCatEstadosDevolucionPedido
{
    public string MdUuid { get; set; } = null!;

    public DateTime MdDate { get; set; }

    public int Id { get; set; }

    public string CodEstadoDevolucion { get; set; } = null!;

    public string? DesEstadoDevolucion { get; set; }

    public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedido>();
}