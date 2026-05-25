namespace SharpAstrology.Enums;

public enum Authorities
{
    Emotional,
    Sacral,
    Splenic,
    EgoManifested,
    EgoProjected,
    SelfProjected,
    Mental,
    Lunar
}

public static class AuthoritiesExtensionMethods
{
    public static string ToText(this Authorities authority) => authority switch
    {
        Authorities.Emotional => "Emotional (Solar Plexus)",
        Authorities.Sacral => "Sacral",
        Authorities.Splenic => "Splenic",
        Authorities.EgoManifested => "Ego Manifested",
        Authorities.EgoProjected => "Ego Projected",
        Authorities.SelfProjected => "Self-Projected",
        Authorities.Mental => "Mental (Sounding Board)",
        Authorities.Lunar => "Lunar (28-Day Cycle)",
        _ => throw new NotImplementedException($"ToText() not implemented for {authority}.")
    };
}
