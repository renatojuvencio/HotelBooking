using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Action
    {
        Pay = 1,
        Finish = 2, //after paid and used
        Cancel = 3, //can mover be paid
        Refound = 4, //paid then refound
        Reopen = 5 //canceled
    }
}
