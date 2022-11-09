using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Estacionamentos.Testes
{
    public class VeiculoTest : IDisposable
    {
        private Veiculo v;
        public ITestOutputHelper SaidaConsoleTeste;
        
        public VeiculoTest(ITestOutputHelper _saidaConsoleTeste)
        {
            v = new Veiculo();
            
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
        }

        [Fact]
        public void TesteVeiculoAcelerarComParametro10()
        {
            //arrange

            //act
            v.Acelerar(10);
            //assert
            Assert.Equal(100, v.VelocidadeAtual);
        }

        [Fact]
        public void TesteVeiculoFrearComParametro10()
        {
            //arrange
            //act
            v.Frear(10);
            //assert
            Assert.Equal(-150, v.VelocidadeAtual);
        }

        [Fact(Skip = "Ainda n�o implementado. Ignorar", DisplayName ="Falta implementar")]
        public void ValidaNomeProprietario(){}

        [Fact]
        public void FichaDeInformacaoDoVeiculo()
        {
            //arrange
            Veiculo v = new()
            {
                Proprietario = "Jose Siqueira",
                Tipo = TipoVeiculo.Automovel,
                Placa = "GAP-9090",
                Cor = "preto",
                Modelo = "variante"
            };
            //act
            string dados = v.ToString();
            //assert
            Assert.Contains("Tipo do Ve�culo: Automovel", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //arrange
            string nomeProprietario = "Ab";

            //assert
            Assert.Throws<System.FormatException>(
                //act
                () => new Veiculo(nomeProprietario)
                );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            string placa = "ASDF8888";
            var msg = Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
                );

            Assert.Equal("O 4� caractere deve ser um h�fen",msg.Message);
        }

        public void Dispose() => SaidaConsoleTeste.WriteLine("Execu��o do Cleanup: Limpando os objetos.");
    }
}