using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAPI.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? LoginName { get; set; }
        public string? Password { get; set; }
        public string? Signature { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
