namespace PropEase.API.Models
{
    public abstract class Imovel
    {
        public int Id { get; set; }

        
        public string Endereco { get; set; } = string.Empty;
        public int Numero { get; set; }
        public bool Alugado { get; set; }

    public int ProprietarioId { get; set; }
    public Proprietario? Proprietario { get; set; }

    public string GetEndereco() => Endereco;
    public void SetEndereco(string endereco) => Endereco = endereco;

    public int GetNumero() => Numero;
    public void SetNumero(int numero) => Numero = numero;

    public bool GetAlugado() => Alugado;
        public void Alugar() => Alugado = true;
        public void Disponibilizar() => Alugado = false;

    public abstract bool EstaAlugado();
    public string ContatoProprietario() => Proprietario != null ? Proprietario.ContatoProprietario() : string.Empty;
        public abstract int CalcularAluguel(int periodo);
        public abstract string EstaAlugadoMensagem();
    }
}
