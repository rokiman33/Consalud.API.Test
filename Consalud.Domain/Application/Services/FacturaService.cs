using AutoMapper;
using Consalud.Model.Dto;
using Consalud.Model.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Domain.Application.Services
{
    public class FacturaService
    {
        private readonly IMapper _mapper;

        public FacturaService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FacturasDTO ConvertToDTO(Facturas factura)
        {
            return _mapper.Map<FacturasDTO>(factura);
        }

        public Facturas ConvertToEntity(FacturasDTO dto)
        {
            return _mapper.Map<Facturas>(dto);
        }

        public async Task<List<FacturasDTO>> LoadFacturaFromJson(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            var facturaModel = JsonConvert.DeserializeObject<List<Facturas>>(jsonContent);

            // Mapear el modelo a DTO
            return _mapper.Map<List<FacturasDTO>>(facturaModel);
        }

    }
}
