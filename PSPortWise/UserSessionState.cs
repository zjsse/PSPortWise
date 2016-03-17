using System.Text;
using PortWiseWS;

namespace PSPortWise
{
    public static class UserSessionState
    {
        public static string PolicyServiceUrl { get; set; }
        public static Subject AuthenticatedSubject { get; set; }
        public static UserAccountService UserAccountService { get; set; }
        public static Encoding Encoding { get; set; } = Encoding.GetEncoding("iso-8859-1");

        public static bool IsAuthenticated
        {
            get
            {
                return AuthenticatedSubject != null;
            }
        }
    }
}
