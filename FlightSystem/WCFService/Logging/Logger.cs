using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Text;

namespace WCFService.Logging {
    public class Logger : TextWriter {
        public override Encoding Encoding {
            get { throw new System.NotImplementedException(); }
        }

        public override void WriteLine(string value) {
            using (var log = new LogDB()) {
                log.Logs.Add(new Log() {Message = value});
                try {
                    log.SaveChanges();
                    this.Flush();
                } catch (DbUpdateException dbUpdateException) {} catch (DbEntityValidationException dbEntityValidationException) {}
            }
        }
    }
}