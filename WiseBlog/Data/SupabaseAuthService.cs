using Supabase;
using System.Threading.Tasks;

namespace WiseBlog.Data
{
    public class SupabaseAuthService
    {
        private readonly Client _client;
        public SupabaseAuthService(Client client)
        {
            _client = client;
        }

        public async Task<bool> Login(string email, string password)
        {
            var session = await _client.Auth.SignIn(email, password);
            return session != null;
        }

        public async Task<bool> Register(string email, string password)
        {
            var session = await _client.Auth.SignUp(email, password);
            return session != null;
        }

        public async Task Logout()
        {
            await _client.Auth.SignOut();
        }

        public bool IsAuthenticated => _client.Auth.CurrentUser != null;
    }
} 