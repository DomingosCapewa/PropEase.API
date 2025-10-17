namespace PropEase.API.Models
{
    public class Apartamento : Imovel
    {
        public override bool EstaAlugado() => Alugado;

        public override string EstaAlugadoMensagem()
            => Alugado
                ? $"O apartamento de número {GetNumero()} está alugado"
                : $"O apartamento de número {GetNumero()} está disponível";

        public override int CalcularAluguel(int periodo)
        {
            int valorBase = 100;
            return periodo > 30 ? (int)(periodo * valorBase * 0.95) : periodo * valorBase;
        }
    }
}
