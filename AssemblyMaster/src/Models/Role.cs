
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssemblyMaster.Models
{
    [Table("ROLES")]
    public class Role
    {
        [Key]
        public Guid ID_ROLE { get; set; }
        public string ROLE_DESC { get; set; }
        public int  LVL_ROLE { get; set; }
        public bool STATUS_CONTA { get; set; }
    }
}


