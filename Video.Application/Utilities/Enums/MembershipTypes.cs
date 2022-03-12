namespace Video.Application.Utilities.Enums;

public class MembershipTypes : Enumeration
{
    public static readonly MembershipTypes PAYASYOUGO = new(1, "Pay As You Go");
    public static readonly MembershipTypes MOUNTHLY = new(2, "Mounthly");
    public static readonly MembershipTypes ANNUALLY = new(3, "Annually");

    protected MembershipTypes(ushort id, string name) : base(id, name)
    {
    }

}
