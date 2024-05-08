# Api User
[![.NET](https://img.shields.io/badge/.NET-8.0.1-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0.1)
[![SQL Server 2019](https://img.shields.io/badge/SQL%20Server-2019-orange.svg)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
[![Docker](https://img.shields.io/badge/Docker-20.10.9-blue.svg)](https://docs.docker.com/engine/install/)
[![Linux](https://img.shields.io/badge/Linux-Ubuntu%2020.04-yellow.svg)](https://ubuntu.com/download/server)
[![WSL](https://img.shields.io/badge/WSL-2-green.svg)](https://docs.microsoft.com/en-us/windows/wsl/install)


## Repositório de Api para manutenção de usuários de uma aplicação apartada

### Descrição
O projeto visa implementar um WebAPI que realiza CRUDS simples de usuários. Em outras palavras, é um serviço web que realiza a manutenção, manipulação e validação de usuários e suas senhas.

### Funcionalidade Principal
  - Criação, Leitura, Alteração e Deleção de dados dos Usuários.
  - Integração com um banco de dados SQL Server hospedado em Containers.

### Tecnologias Utilizadas
- ASP .NET Core
- Serviços RESTful
- SQL Server 2019
- Docker
- Linux

### Frameworks e Bibliotecas Adicionais
- Swashbuckle.AspNetCore
- Microsoft.AspNetCore.OpenApi
- Newtonsoft.Json
- Microsoft.EntityFrameworkCore
- Microsot.Data.SqlClient
- SwashBucle.AspNetCore

# ApiController

## Controllers
- Utilização de Try-Catch em cada Endpoint
- Encapsulamento do objeto de Serviço

```csharp
namespace AssemblyMaster.Controllers
{
    
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] UserLoginModel user)
        {
            try
            {
                var result = _userService.CreateService(user.Password, user.Email);
                if (string.IsNullOrEmpty(result)) 
                    return Ok("Usuário cadastrado com Sucesso");
                else 
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }
        }
```

## Services
Utilizados para segregar as responsabilidades de cada chamada de cada Endpoint.

```csharp

namespace AssemblyMaster.Services
{
    public class UserService 
    {    
        public string CreateService(string password, string email)
        {
            try
            {
                PasswordValidator.Content(password);
                EmailValidator.Content(email);
                var userRepository = new UserRepository();
                return userRepository.Create(new User(email, password, "systemIntegration")); 
            }
            catch(ApplicationException ex)
            {
                return ex.Message;
            }
        }

        public string PasswordService(string password, string email)
        {
            try
            {
                PasswordValidator.Content(password);
                EmailValidator.Content(email);
                var updatesql = new UserRepository();
                return updatesql.Update(new User(email, password, "systemIntegration")); 
            }
            catch(ApplicationException e)
            {
                return e.Message;
            }
        }

        public IEnumerable<User> ReadService()
        {
            var ReadUser = new UserRepository();
            return ReadUser.Read(); 
        }

        public string DeleteUserService(Guid guid)
        {
            try
            {
                var DeleteUser = new UserRepository();
                return DeleteUser.Delete(guid);
            }
            catch(ApplicationException e)
            {
                return e.Message;
            }
        }

        public User ReadGuidService(Guid guid)
        {
            var GetRepository = new UserRepository();
            return GetRepository.ReadId(guid); 
        }

        public bool LoginService(string password, string email)
        {
            var loginRepository = new UserRepository();
            return loginRepository.LoginUser(email, password);
        }
    }
}

```

## Entitieis/Models
Define todos os atributos e métodos de uma entidade. Está diretamente ligado a cada tabela do Banco de Dados, para facilitar o mapeamento pelo ORM.

```csharp

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssemblyMaster.Security; 

namespace AssemblyMaster.Models
{
    [Table("USERS")]
    public class User
    {
        [Key]
        public Guid ID { get; set; }
        public string LOGIN { get; set; }
        public string NOME_COMPLETO { get; set; }
        public string EMAIL { get; set; }
        public string  PASSWORD { get; set; }
        public string  CPF { get; set; }
        public DateTime DATA_NASCIMENTO { get; set; }
        public DateTime DATA_CADASTRO {get; set;}
        public string LOGIN_CADASTRO { get; set; }
        public DateTime ULTIMO_LOGIN { get; set; }
        public bool STATUS_CONTA {get; set;}
        public string  TELEFONE { get; set; }
        public string ENDERECO { get; set; }
        public char GENERO { get; set; }
        public string SALT { get; set; }

        //Sobrecarga de Construtores
         public User(string email, string password, string logincadastro)
        {
            ID = Guid.NewGuid();
            SALT = PasswordHasher.GenerateSalt();
            EMAIL = email;
            LOGIN = email.Substring(0, email.IndexOf("@"));
            PASSWORD = PasswordHasher.HashPassword(password, SALT);
            DATA_CADASTRO = DateTime.Now;
            LOGIN_CADASTRO = logincadastro;
            ULTIMO_LOGIN = DateTime.Now;
            STATUS_CONTA = true;
            TELEFONE = "";
            NOME_COMPLETO = "";
            CPF = "";
            ENDERECO = "";
        }
        public User(){}
        public User(string password, string salt)
        {
            PASSWORD = password;
            SALT = salt;
        }
    }
}
```

## Context
Define um modelo a ser seguido para configuração do Entity Framework. Isto é, para que o mapeamento das Entidades-Relacionais seja bem sucedido.

```csharp
using AssemblyMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyMaster.Data
{
    //Herda do Objeto DbContext
    public class MkmDataContext : DbContext 
    {
        //Cada Propriedade equivale a uma tabela no Banco
        public DbSet<User> USERS { get; set; }
        public DbSet<Role> ROLES { get; set; }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"));
        }
    }
}
```

# Conclusão
O projeto visa adequar todas as funcionalidades de um api de cadastro de usuário. Nesse webservice é possível persistir dados numa base sólida, manipular os mesmos e deletar.
Utilizando metodologias de autenticação, qualquer aplicação que tenha acesso a esse serviço poderá, desde que esteja configurada, consumir os Endpoints apresentados. 
