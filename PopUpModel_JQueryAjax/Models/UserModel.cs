using System.ComponentModel.DataAnnotations;

namespace PopUpModel_JQueryAjax.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Department { get; set; }
    }
}
