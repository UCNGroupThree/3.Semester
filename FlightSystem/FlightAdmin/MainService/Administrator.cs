using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAdmin.MainService {
    public partial class Administrator {

        public override string ToString() {
            return string.Format("{0} #{1}", Username, ID);
        }

        public Administrator GetCopy() {
            return new Administrator {
                ExtensionData = ExtensionData,
                Concurrency = Concurrency,
                ID = ID,
                Username = Username,
                PasswordPlain = PasswordPlain
            };
        }

        public void SetToCopy(Administrator copy) {
            ExtensionData = copy.ExtensionData;
            Concurrency = copy.Concurrency;
            ID = copy.ID;
            Username = copy.Username;
            PasswordPlain = copy.PasswordPlain;
        }
    }
}
