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
