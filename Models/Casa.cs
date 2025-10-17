namespace PropEase.API.Models
{
    public class Casa : Imovel
    {
        public override bool EstaAlugado() => Alugado;

        public override string EstaAlugadoMensagem()
            => Alugado ? "A casa está alugada" : "A casa está disponível";

        public override int CalcularAluguel(int periodo)
            => periodo * 120; 
    }
}
