using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models;

public class GymMember
{
    public int Id { get; set; }

    [Display(Name = "Ho ten")]
    [Required(ErrorMessage = "Ten hoi vien khong duoc de trong")]
    [StringLength(80, ErrorMessage = "Ten hoi vien toi da 80 ky tu")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email khong duoc de trong")]
    [EmailAddress(ErrorMessage = "Email khong dung dinh dang")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "So dien thoai")]
    [Required(ErrorMessage = "So dien thoai khong duoc de trong")]
    [Phone(ErrorMessage = "So dien thoai khong dung dinh dang")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Goi tap")]
    [Required(ErrorMessage = "Goi tap khong duoc de trong")]
    [StringLength(60, ErrorMessage = "Goi tap toi da 60 ky tu")]
    public string PackageName { get; set; } = string.Empty;

    [Display(Name = "Phi thang")]
    [Range(1, 100000000, ErrorMessage = "Phi thang phai lon hon 0")]
    public decimal MonthlyFee { get; set; }

    [Display(Name = "So buoi con lai")]
    [Range(0, 365, ErrorMessage = "So buoi con lai phai lon hon hoac bang 0")]
    public int SessionsLeft { get; set; }

    [Display(Name = "Ngay bat dau")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Today;
}
