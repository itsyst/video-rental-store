namespace Video.Domain.Enums;

public class MembershipTypes : Enumeration
{
    public static readonly MembershipTypes PAYASYOUGO = new(1, "Pay As You Go");
    public static readonly MembershipTypes MONTHLY = new(2, "Monthly");
    public static readonly MembershipTypes ANNUALLY = new(3, "Annually");

    protected MembershipTypes(ushort id, string name) : base(id, name)
    {
    }

}
