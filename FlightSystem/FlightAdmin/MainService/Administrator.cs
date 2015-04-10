using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAdmin.MainService {
    public partial class Administrator {
        
        public Administrator GetCopy() {
            return new Administrator {
                ID = ID,
                Username = Username,
                PasswordPlain = PasswordPlain
            };
        }

        public void SetToCopy(Administrator copy) {
            ID = copy.ID;
            Username = copy.Username;
            PasswordPlain = copy.PasswordPlain;
        }
    }
}
