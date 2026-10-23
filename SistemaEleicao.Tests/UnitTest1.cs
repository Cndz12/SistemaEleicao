
using SistemaEleicao.Console;

namespace SistemaEleicao.Tests;

public class UnitTest1
{
    [Fact]
    public void NaoDeveCadastrarCandidatosComMesmoNumero()
    {
        var eleicao = new EleicaoManager();

        Assert.True(eleicao.CadastrarCandidato("Ana", 10));
        Assert.False(eleicao.CadastrarCandidato("Carlos", 10));
    }

    [Fact]
    public void EleitorNaoPodeVotarDuasVezes()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.RegistrarEleitor("123");

        Assert.True(eleicao.Votar("123", 10));
        Assert.False(eleicao.Votar("123", 10));
    }

    [Fact]
    public void VotoEmCandidatoInexistenteDeveSerNulo()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor("123");

        Assert.True(eleicao.Votar("123", 99));
        Assert.Equal(1, eleicao.VotosNulos);
    }

    [Fact]
    public void DeveContabilizarVotoBranco()
    {
        var eleicao = new EleicaoManager();

        eleicao.RegistrarEleitor("123");

        Assert.True(eleicao.Votar("123", null));
        Assert.Equal(1, eleicao.VotosBrancos);
    }
}
