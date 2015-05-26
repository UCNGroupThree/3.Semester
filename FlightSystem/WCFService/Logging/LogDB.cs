using System.Data.Entity;

namespace WCFService.Logging {
    public class LogDB : DbContext {

        //private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();

        public LogDB()
            : base("Server=kraka.ucn.dk;Database=dmaa0214_3Sem_3;User Id=dmaa0214_3Sem_3;Password=IsAllowed;") {
            base.Configuration.ProxyCreationEnabled = false;

        }


        public virtual DbSet<Log> Logs { get; set; }

    }
}