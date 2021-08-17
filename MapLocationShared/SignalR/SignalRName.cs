
namespace MapLocation.Shared.SignalR
{
    public class SignalRName
    {
        #region Rotas
        public const string RouteLocationHub = "/locationhub";
        #endregion

        #region Grupos
        public const string Group = nameof(Group);
        #endregion

        #region Metodos Server
        public const string JoinGroup = nameof(JoinGroup);
        #endregion

        #region Metodos Client
        public const string LocationHub = nameof(LocationHub);
        #endregion
    }
}
