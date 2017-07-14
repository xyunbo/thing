using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thing
{
    class selectedRow
    {
        private int sRow = -1;
        public void setRow(int arg)
        {
            sRow = arg;
        }
        public int getRow()
        {
            return sRow;
        }
    }
}
