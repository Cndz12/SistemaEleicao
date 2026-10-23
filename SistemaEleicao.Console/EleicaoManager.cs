
using SistemaEleicao.Console.Models;

namespace SistemaEleicao.Console;

public class EleicaoManager
{
    private readonly List<Candidato> candidatos = new();
    private readonly HashSet<string> eleitores = new();

    public int VotosBrancos { get; private set; }
    public int VotosNulos { get; private set; }

    public IReadOnlyList<Candidato> Candidatos => candidatos;

    public bool CadastrarCandidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome) ||
            numero <= 0 ||
            candidatos.Any(c => c.Numero == numero))
        {
            return false;
        }

        candidatos.Add(new Candidato(nome, numero));
        return true;
    }

    public bool RegistrarEleitor(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            return false;

        return eleitores.Add(documento.Trim());
    }

    public bool Votar(string documento, int? numeroCandidato)
    {
        if (string.IsNullOrWhiteSpace(documento))
            return false;

        documento = documento.Trim();

        if (!eleitores.Contains(documento))
            return false;

        if (numeroCandidato == null)
        {
            VotosBrancos++;
        }
        else
        {
            var candidato = candidatos.FirstOrDefault(
                c => c.Numero == numeroCandidato.Value);

            if (candidato == null)
                VotosNulos++;
            else
                candidato.ReceberVoto();
        }

        eleitores.Remove(documento);
        return true;
    }

    public Candidato? ObterVencedor()
    {
        return candidatos
            .OrderByDescending(c => c.Votos)
            .FirstOrDefault();
    }
}
