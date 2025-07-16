using System.Threading.Tasks;
using Supabase;
using Supabase.Gotrue;

namespace WiseBlog.Data
{
    public class SupabaseAuthService
    {
        private readonly Supabase.Client _client;  // fully qualified

        public SupabaseAuthService(Supabase.Client client)
        {
            _client = client;
        }

        public async Task<bool> Register(string email, string password)
        {
            var session = await _client.Auth.SignUp(email, password);
            return session?.User != null;
        }

        public async Task<bool> Login(string email, string password)
        {
            var session = await _client.Auth.SignIn(email, password);
            return session?.User != null;
        }

        public async Task Logout()
        {
            await _client.Auth.SignOut();
        }

        public bool IsAuthenticated => _client.Auth.CurrentUser != null;

        public User? GetCurrentUser() => _client.Auth.CurrentUser;
    }
}