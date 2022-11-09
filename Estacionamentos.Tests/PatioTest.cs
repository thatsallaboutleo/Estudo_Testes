using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Estacionamentos.Testes
{
    public class PatioTest : IDisposable
    {

        private Veiculo v;
        private Patio est;
        private Operador op;
        public ITestOutputHelper SaidaConsoleTeste;

        public PatioTest(ITestOutputHelper _saidaConsoleTeste)
        {
            v = new Veiculo();
            est = new Patio();
            op = new Operador
            {
                Nome = "Herold da Silva"
            };

            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");

        }

        [Fact(DisplayName = "Validação do faturamento")]
        public void ValidaFaturamento()
        {
            Patio p = new();
            Veiculo v = new();

            v.Proprietario = "Andre Silva";
            v.Tipo = TipoVeiculo.Automovel;
            v.Cor = "Azul";
            v.Modelo = "Marea";
            v.Placa = "asd-9090";

            p.OperadorPatio = op;
            p.RegistrarEntradaVeiculo(v);
            p.RegistrarSaidaVeiculo(v.Placa);

            double fat = p.TotalFaturado();

            Assert.Equal(2,fat);
        }

        [Theory(DisplayName = "Faturamento Total")]
        [InlineData("Jackie Chan","ASD-2030","preto","marea")]
        [InlineData("Silvester Stalone", "ASD-4050", "prata","corsa")]
        [InlineData("Poze do Rodo", "CPX-6070", "branco","golf")]
        public void ValidaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //arrange
            Patio est = new();
            Veiculo car = new()
            {
                Proprietario = proprietario,
                Placa = placa,
                Cor = cor,
                Modelo = modelo
            };

            est.OperadorPatio = op;
            est.RegistrarEntradaVeiculo(car);
            est.RegistrarSaidaVeiculo(car.Placa);

            //act
            double fat = est.TotalFaturado();

            //assert
            Assert.Equal(2, fat);
        }

        [Theory(DisplayName ="Localizar pela placa")]
        [InlineData("Poze do Rodo", "CPX-6070", "branco", "golf")]
        public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string proprietario, string placa, string cor, string modelo)
        {
            //arrange
            Patio est = new();
            Veiculo car = new()
            {
                Proprietario = proprietario,
                Placa = placa,
                Cor = cor,
                Modelo = modelo
            };
            est.OperadorPatio = op;
            est.RegistrarEntradaVeiculo(car);

            //act
            var consultar = est.PesquisaVeiculo(car.IdTicket);

            //assert
            Assert.Contains("### Ticket de Estacionamento ###", consultar.Ticket);

        }

        [Fact(DisplayName ="Alterar os dados do veículo")]
        public void AlterarDadosVeiculoDoProprioVeiculo()
        {
            //arrange
            Patio est = new();
            Veiculo v = new()
            {
                Proprietario = "Ronaldinho",
                Placa = "BRA-7879",
                Cor = "prata",
                Modelo = "corsa"
            };
            est.OperadorPatio = op;
            est.RegistrarEntradaVeiculo(v);

            Veiculo v2 = new()
            {
                Proprietario = "Ronaldinho",
                Placa = "BRA-7879",
                Cor = "preto", //alterado
                Modelo = "corsa"
            };

            //act
            Veiculo alterado = est.AlterarDadosVeiculo(v2);

            //assert
            Assert.Equal(alterado.Cor, v2.Cor);
        }

        public void Dispose() => SaidaConsoleTeste.WriteLine("Execução do Cleanup: Limpando os objetos.");
    }
}
