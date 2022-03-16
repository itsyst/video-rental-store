using System.ComponentModel.DataAnnotations;

namespace Video.Domain.Entities;

public class MembershipType
{
    [Key]
    public byte Id { get; set; }
    public short SignUpFee { get; set; }

    [DataType(DataType.DateTime)]
    public byte Duration { get; set; }
    public byte DiscountRate { get; set; }
 }
