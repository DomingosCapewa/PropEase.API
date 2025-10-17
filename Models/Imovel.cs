namespace PropEase.API.Models
{
    public abstract class Imovel
    {
       
        protected int id;
        protected string endereco = string.Empty;
        protected int numero;
        protected bool alugado;
        protected Proprietario? proprietario;

      
        public int Id { get => id; set => id = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public int Numero { get => numero; set => numero = value; }
       
        public bool Alugado { get => alugado; private set => alugado = value; }

        
        public int ProprietarioId { get; set; }
        public Proprietario? Proprietario { get => proprietario; set => proprietario = value; }

        
        public abstract decimal CalcularAluguel(int dias);

       
        public virtual string ObterStatusAluguel()
        {
            return alugado ? "Alugado" : "Disponível";
        }

       
        public void Alugar()
        {
            alugado = true;
        }

        public void Disponibilizar()
        {
            alugado = false;
        }
    }
}
