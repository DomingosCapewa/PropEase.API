namespace PropEase.API.Models
{
    public class Apartamento : Imovel
    {
       
        public override decimal CalcularAluguel(int dias)
        {
            const decimal valorDiaria = 100m;
            decimal valor = dias * valorDiaria;
            return valor;
        }

        public override string ObterStatusAluguel()
        {
            return alugado
                ? $"O apartamento nº {numero} está alugado"
                : $"O apartamento nº {numero} está disponível";
        }
    }
}
