
namespace MapLocation.Shared.SignalR
{
    public class SignalRName
    {
        #region Rotas
        public const string RouteLocationHub = "/locationhub";
        public const string RouteTesteHub = "/testehub";
        #endregion

        #region Grupos
        public const string Group = nameof(Group);
        #endregion

        #region Metodos Server
        public const string JoinGroup = nameof(JoinGroup);
        public const string SaveLocationNotify = nameof(SaveLocationNotify);

        public const string Message = nameof(Message);
        #endregion

        #region Metodos Client
        public const string LocationHub = nameof(LocationHub);
        public const string TestMessageHub = nameof(TestMessageHub);
        #endregion
    }
}
