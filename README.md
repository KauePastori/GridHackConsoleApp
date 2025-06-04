⚡️ GridHack - Simulador Interativo de Falhas em Redes Elétricas

GridHack é uma aplicação desenvolvida em C#/.NET 6 que simula falhas, isolamento e restauração de nós em uma rede elétrica, com pontuação, ranking e histórico persistente.

🚀 Objetivo
Treinar e educar operadores e estudantes sobre o isolamento de falhas e a manutenção da conectividade em redes elétricas.

Reforçar conceitos de resiliência, decisão em tempo real e impactos cibernéticos no setor energético.

🛠️ Tecnologias Utilizadas
| Tecnologia       | Descrição                    |
| ---------------- | ---------------------------- |
| **C# (.NET 6)**  | Backend da aplicação console |
| System.Text.Json | Persistência local (JSON)    |
| Stopwatch        | Medição do tempo de execução |
| System.Console   | Saída formatada com cores    |
| Mermaid.js       | Diagrama de fluxo (Markdown) |
| Visual Studio    | Ambiente de desenvolvimento  |
| Git/GitHub       | Versionamento e publicação   |


✅ Funcionalidades Principais
🔐 Login com autenticação
🖥️ Visualização dinâmica da rede com cores
🧩 Isolamento/restauração de nós com lógica de validação
🧠 Verificação de conectividade da rede em tempo real
🏁 Finalização com cálculo de pontuação e Rank
📊 Histórico persistido em JSON
🔥 Simulação de ataques SCADA (voltagem/corrente anormal)


📥 Instalação e Execução

Clone o repositório:
git clone https://github.com/seu-usuario/GridHack.git
cd GridHack

Compile e execute:
dotnet build
dotnet run


🧪 Exemplo de Uso
🔐 Login:
Usuário: user
Senha: 1234

⚡️ REDE ELÉTRICA - STATUS ATUAL ⚡️

[1] ❌ Falha
[2] ✅ OK
[3] ❌ Falha
[4] ✅ OK
[5] ✅ OK

🌐 Rede conectada: ✔️ Sim

Digite o ID do nó para isolar/restaurar ou '0' para voltar:
> 1

✅ Nó 1 isolado com sucesso!

Pressione qualquer tecla para continuar...


