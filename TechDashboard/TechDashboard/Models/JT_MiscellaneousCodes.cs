using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDashboard.Models
{
    public class JT_MiscellaneousCodes
    {
        /// <summary>
        /// Record Type - varchar(1)
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// Code Type - varchar(2)
        /// </summary>
        public string CodeType { get; set; }

        /// <summary>
        /// Miscellaneous Code - varchar(6)
        /// </summary>
        public string MiscellaneousCode { get; set; }

        /// <summary>
        /// Description - varchar(40)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional Description Numeric - varchar(40)
        /// </summary>
        public string AddtlDescNum { get; set; }

        public override string ToString()
        {
            return Description;
            //return string.Format("[JT_MiscellaneousCodes: RecordType={0}, CodeType={1}, MiscellaneousCode={2}, Description={3}, AddtlDescNum={4}]", RecordType, CodeType, MiscellaneousCode, Description, AddtlDescNum);
        }
    }
}
