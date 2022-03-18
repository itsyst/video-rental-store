using System.ComponentModel.DataAnnotations;

namespace Video.Application.Profiles.Dtos;

public class MembershipTypeDto
{
    [Key]
    public byte Id { get; set; }
    public short SignUpFee { get; set; }

    [DataType(DataType.DateTime)]
    public byte Duration { get; set; }
    public byte DiscountRate { get; set; }
}
