
using SistemaEleicao.Console;

var eleicao = new EleicaoManager();

while (true)
{
    Console.WriteLine("\n=== SISTEMA DE ELEIÇÃO ===");
    Console.WriteLine("1 - Cadastrar candidato");
    Console.WriteLine("2 - Registrar eleitor");
    Console.WriteLine("3 - Votar");
    Console.WriteLine("4 - Apurar votos");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha: ");

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Nome do candidato: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Número: ");
            if (int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine(
                    eleicao.CadastrarCandidato(nome, numero)
                    ? "Candidato cadastrado!"
                    : "Cadastro inválido ou número já utilizado.");
            }
            else
            {
                Console.WriteLine("Número inválido.");
            }
            break;

        case "2":
            Console.Write("Título/CPF simulado: ");
            string documento = Console.ReadLine() ?? "";

            Console.WriteLine(
                eleicao.RegistrarEleitor(documento)
                ? "Eleitor registrado!"
                : "Documento inválido ou já registrado.");
            break;

        case "3":
            Console.Write("Título/CPF do eleitor: ");
            string eleitor = Console.ReadLine() ?? "";

            Console.Write("Número do candidato (0 para branco): ");
            if (int.TryParse(Console.ReadLine(), out int voto))
            {
                Console.WriteLine(
                    eleicao.Votar(eleitor, voto == 0 ? null : voto)
                    ? "Voto registrado!"
                    : "Eleitor não registrado ou já votou.");
            }
            else
            {
                Console.WriteLine("Voto inválido.");
            }
            break;

        case "4":
            Console.WriteLine("\n=== RESULTADO ===");

            foreach (var candidato in eleicao.Candidatos)
            {
                Console.WriteLine(
                    $"{candidato.Nome} ({candidato.Numero}): " +
                    $"{candidato.Votos} votos");
            }

            Console.WriteLine($"Brancos: {eleicao.VotosBrancos}");
            Console.WriteLine($"Nulos: {eleicao.VotosNulos}");

            var vencedor = eleicao.ObterVencedor();

            if (vencedor == null)
                Console.WriteLine("Nenhum candidato cadastrado.");
            else
                Console.WriteLine($"Mais votado: {vencedor.Nome}");

            break;

        case "0":
            return;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}
