using AssemblyMaster.Exceptions;
using AssemblyMaster.Models;
using AssemblyMaster.Repositories;
using AssemblyMaster.Validators;

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
    

