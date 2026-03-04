# 🔐🏦 Encrypted Banking System (Console App - C#)
Sistema de banco com autenticação desenvolvido em C# seguindo boas práticas de programação, 
foco em arquitetura e código devidamente organizado.
Armazenamento em banco de dados PostgresSQL e uso de hash de senhas.
## 🎯 Objetivo
### Este projeto tem como finalidade:
* Praticar orientação à objetos
* Implementar fluxo de autenticação
* Trabalhar com persistência de dados
* Aplicar hash seguro de senha (BCrypt)
* Implementar diferentes disponibilidades de idiomas

## 🏗  Arquitetura:
O sistema tem a seguinte separação lógica:
* Model
* Service
* Program

## 🚀 Funcionalidades:
✔ Cadastro de usuário
<br>
✔ Login com validação de credenciais
<br>
✔ Hash de senha com BCrypt
<br>
✔ Exclusão por ID (por número da conta)
<br>
✔ Update parcial de usuário (mudar valor da renda + limite de crédito)

## 🔐 Segurança:
* Senhas não são armazenadas em texto puro
* Utilização de BCrypt para hashing
* Separação entre regra de negócio e persistência
* 
## 🧠 Conceitos Aplicados:
* CRUD completo (Create, Read, Update**, Delete**)
* Auto-increment/Sequence de banco
* Boas práticas básicas de organização
### Preparação para futura implementação de
* Logs de auditoria
* API REST
* Interface Web
* Multi-idiomas
* 
## 💻 Execução:
Projeto executado como Console Application (.NET)
```
dotnet run
```
## 👨‍💻 Tecnologias
### C# 🟨
Linguagem principal utilizada no desenvolvimento do sistema.

### NET (.NET / .NET Core) 🟪
Plataforma utilizada para execução da aplicação Console.

### Entity Framework Core 🟩
ORM utilizado para mapeamento objeto-relacional e comunicação com o banco de dados.

### PostgreSQL 🐘
Sistema de gerenciamento de banco de dados relacional.

### BCrypt 🔐
Algoritmo utilizado para hash seguro de senhas.

### DBeaver ⬜
Ferramenta utilizada para administração e consulta do banco de dados.

## 📦 Instalação Dos Pacotes
Devem ser instalados usando: 
* `dotnet add package Microsoft.EntityFrameworkCore`
* `dotnet add package Microsoft.EntityFrameworkCore.Tools`
* `dotnet add package Microsoft.EntityFrameworkCore.Design`
* `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`
* `dotnet tool install --global dotnet-ef`
* `dotnet add package BCrypt.Net-Next`

## 💡📋 Separação de Responsábilidades
A arquitetura segue uma estrutura bem simples:
<br>
Encryption📁
<br>
├──Data📁
<br>
⠀⠀└──AppContextDb.cs📄
<br>
├──Migrations📁
<br>
⠀⠀└──[...]
<br>
├──Models📁
<br>
⠀⠀└──Account.cs📄
<br>
├──Services📁
<br>
⠀⠀├──Auth.cs📄
<br>
⠀⠀├──NullRm.cs📄
<br>
⠀⠀├──Operations.cs📄
<br>
⠀⠀├──Response.cs📄
<br>
├──UI📁
<br>
⠀⠀├──Lang📁
<br>
⠀⠀⠀⠀└──Yay.cs📄
<br>
⠀⠀└──Prompt📁
<br>
⠀⠀⠀⠀└──Menu.cs📄
<br>
├──Program.cs📄
<br>
## Partes Principais
### Program.cs📄
Classe principal que executa e controla todas as outras.

### Models📁/Account.cs📄
Classe que cria o objeto **Account**, controla parte dos dados e os repassa para Auth📄

### Services📁/Auth.cs📄
Classe que cria/manipula dados e acessa o banco de dados.

### Services📁/Operations.cs📄
Classe que faz as operações bancárias

### Services📁/Response.cs📄
Classe que padroniza respostas e saídas do prompt para o usuário

### UI📁/Prompt📁/Menu.cs📄
Menu bancário

### Services📁/NullRm.cs📄
Classe simples que remove "Byte Nulos" do input do usuário
<br>
**Funcionamento:**
```
public class NullRm
    {
        public static string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            {
                return input.Replace("\0", string.Empty).Trim();
            }
        }
    }
```
