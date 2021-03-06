//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.BookRatings = new HashSet<BookRatings>();
            this.Orders = new HashSet<Orders>();
            this.RecomBooks = new HashSet<RecomBooks>();
        }
    
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string LoginPwd { get; set; }
        public string hs_pwd { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public int UserRoleId { get; set; }
        public int UserStateId { get; set; }
        public string RegisterIp { get; set; }
        public System.DateTime RegisterTime { get; set; }
        public System.DateTime Uptime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookRatings> BookRatings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecomBooks> RecomBooks { get; set; }
        public virtual UserRoles UserRoles { get; set; }
        public virtual UserStates UserStates { get; set; }
    }
}
