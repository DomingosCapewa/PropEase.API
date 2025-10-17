namespace PropEase.API.Models
{
    public class Proprietario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;

        public string ContatoProprietario() => $"{Nome} - {Telefone}";
    }
}
