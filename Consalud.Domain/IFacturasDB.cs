﻿using AutoMapper;
using Consalud.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Domain
{
    public interface IFacturasDB
    {

        Task<List<FacturasDTO>> LoadDB();
    }
}
