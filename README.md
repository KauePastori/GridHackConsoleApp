# ⚡️ GRIDHACK – Simulador Interativo de Falhas em Redes Elétricas

## 📌 Descrição
GridHack é um simulador interativo desenvolvido em **C#/.NET 6** para treinar operadores e estudantes sobre identificação, isolamento e restauração de falhas em redes elétricas. O sistema permite simular eventos críticos, acompanhar pontuações, histórico de partidas e testar decisões em tempo real – tudo em uma interface console colorida, robusta e fácil de usar.

---

## 🎯 Objetivo

- **Treinar** operadores e estudantes em procedimentos de isolamento de falhas e manutenção da conectividade em redes elétricas.
- **Reforçar** conceitos de resiliência, decisão sob pressão e impactos cibernéticos no setor energético.
- **Simular** situações reais, incluindo ataques SCADA e falhas inesperadas, promovendo educação em segurança e continuidade operacional.

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia        | Descrição                                 |
|-------------------|-------------------------------------------|
| **C# (.NET 6)**   | Backend da aplicação (console)            |
| System.Text.Json  | Persistência local de dados (JSON)        |
| Stopwatch         | Medição de tempo de execução              |
| System.Console    | Interface colorida e formatada            |
| Mermaid.js        | Diagramas de fluxo para documentação      |
| Visual Studio     | Ambiente de desenvolvimento recomendado   |
| Git/GitHub        | Versionamento e publicação                |

---

## ✅ Funcionalidades Principais

- 🔐 **Login seguro:** autenticação básica do usuário  
- 🖥️ **Visualização dinâmica:** rede exibida com cores e status em tempo real  
- 🧩 **Isolamento/restauração de nós:** lógica de validação e simulação fiel  
- 🧠 **Verificação de conectividade:** checagem contínua da integridade da rede  
- 🏁 **Pontuação e ranking:** feedback imediato ao final de cada simulação  
- 📊 **Histórico persistente:** resultados armazenados em arquivo JSON local  

---
## 🔐 Login:
- **Usuário:** user
- **Senha:** 1234

## ⚡️ REDE ELÉTRICA - STATUS ATUAL ⚡️

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

---
## Link Youtube : https://youtu.be/B9gHjFhlb5M

## 📥 Instalação e Execução

```bash
git clone https://github.com/KauePastori/GridHack.git
cd GridHack
dotnet build
dotnet run


