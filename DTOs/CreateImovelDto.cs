namespace PropEase.API.DTOs
{
    public class CreateImovelDto
    {
        
        public string Tipo { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;
        public int Numero { get; set; }

        
        public int ProprietarioId { get; set; }

        
        public bool Alugado { get; set; } = false;
    }
}