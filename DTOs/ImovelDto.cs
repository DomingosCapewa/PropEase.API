using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropEase.API.DTOs
{
    public class ImovelDto
    {

        public int Id { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public int Numero { get; set; }
        public bool Alugado { get; set; }
        public string ProprietarioNome { get; set; } = string.Empty;
        public string ProprietarioTelefone { get; set; } = string.Empty;
    }
}

