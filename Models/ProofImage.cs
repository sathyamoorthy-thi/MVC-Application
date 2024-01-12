using System.ComponentModel.DataAnnotations;


namespace ReimbursementClaim
{


public class Proof
{
    [Key]
    public int Id { get; set; }

    [DataType(DataType.Upload)]
    [Required(ErrorMessage = "Proof is required")]  
    public byte[] ImageData { get; set; }
}
}