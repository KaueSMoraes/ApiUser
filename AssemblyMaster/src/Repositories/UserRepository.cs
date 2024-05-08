using AssemblyMaster.Models;
using AssemblyMaster.Data;
using AssemblyMaster.Security;
using Microsoft.EntityFrameworkCore;
using AssemblyMaster.Interfaces;

namespace AssemblyMaster.Repositories
{
    public class UserRepository : ICrud<User>
    {
        public string Create(User user)
        {
            try
            {
                using(var context = new MkmDataContext())
                {
                    context.Add(user);
                    return (context.SaveChanges() >= 1) ? "" : "Falha ao cadastrar usuário";
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
         }

        public IEnumerable<User> Read()
        {
          try
          {
             using(var context = new MkmDataContext())
             {
                var Users = context.USERS
                .Where(x => x.STATUS_CONTA) //Filtra primeiro em Cache, para executar query com "WHERE"
                .AsNoTracking() //Utilizo AsNoTrackong para a query não retornar Metadados desnecessários
                .ToList();
            
                return Users;
             }  
          }
          catch(ApplicationException)
          {
            return new List<User>();
          }
        }     
    
        //Sobrecarga do Read para buscar por ID
        public User ReadId(Guid guid)
        {
            try
            {
                using(var context = new MkmDataContext())
                {
                   var userguid = context.USERS.FirstOrDefault(x => x.ID == guid);
                   return (userguid != null) ? userguid : new User();
                }
            }
            catch(ApplicationException)
            {
                return new User();
            }
        }     

        public string Update(User user)
        {
            try
            {
                using(var context = new MkmDataContext())
                {
                    //Pego o objeto direto do Banco para criar a query de Update.
                    //Utilizo Linq para filtrar na lista do Context que foi lido do Banco.
                    var storedUser = context.USERS.FirstOrDefault(x => x.EMAIL == user.EMAIL);
                    storedUser.PASSWORD = user.PASSWORD;

                    //Realiza update com base no objeto manipulado pelo EF
                    context.Update(storedUser);
                    return (context.SaveChanges() >= 1) ? "Usuário atualizado com sucesso" : "Falha ao atualizar usuário";
                }
            }
            catch(Exception e)
            {
               return e.Message;
            }   
        }

        public string Delete(Guid guid)
        {
            try
            {
                using(var context = new MkmDataContext())
                {
                    var storedUser = context.USERS.FirstOrDefault(x => x.ID == guid);
                    context.Remove(storedUser);
                    return (context.SaveChanges() >= 1) ? "Usuário deletado com sucesso" : "Falha ao excluir usuário";
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public bool LoginUser(string email, string password)
        {
            try
            {
                using (var context = new MkmDataContext())
                {
                    var storedUser = context.USERS.AsNoTracking().FirstOrDefault(x => x.EMAIL == email);
                    var newPassword = PasswordHasher.HashPassword(password, storedUser.SALT);

                    if (storedUser != null)
                        if(storedUser.PASSWORD == newPassword)
                            return true; //Login bem sucedido
                        else
                            return false; //Senha Incorreta
                    else 
                        return false; //Login Incorreto, pois não existe no banco
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao realizar o login: " + ex.Message);
                return false;
            }
        }
    }
}
