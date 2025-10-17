namespace PropEase.API.Models
{
    public class Casa : Imovel
    {
       
        public override decimal CalcularAluguel(int dias)
        {
            const decimal valorDiaria = 120m;
            return dias * valorDiaria;
        }

        
        public override string ObterStatusAluguel()
        {
            return alugado ? "A casa está alugada" : "A casa está disponível";
        }
    }
}
