using SharpAstrology.Enums;
using SharpAstrology.ExtensionMethods;
using SharpAstrology.HumanDesign.Mathematics;
using SharpAstrology.Interfaces;
using SharpAstrology.Utility;

namespace SharpAstrology.DataModels;

public sealed class HumanDesignCompositeChart : IHumanDesignChart
{
    private readonly HashSet<Gates> _p1ActiveGates;
    private readonly HashSet<Gates> _p2ActiveGates;
    
    /// <summary>
    /// Gets a dictionary of personality activations corresponding to each celestial body for person 1. 
    /// </summary>
    public Dictionary<Planets, Activation> P1PersonalityActivation { get; }
    
    /// <summary>
    /// Gets a dictionary of planetary fixing states for each personality planet of person 1.
    /// The value will be calculated on the first call of this property.
    /// </summary>
    public Dictionary<Planets, PlanetaryFixation> P1PersonalityFixation
    {
        get
        {
            field ??= _planetaryFixations(P1PersonalityActivation, P1DesignActivation, P2PersonalityActivation, P2DesignActivation);
            return field;
        }
    }
    
    /// <summary>
    /// Gets a dictionary of design activations corresponding to each celestial body for person 1. 
    /// </summary>
    public Dictionary<Planets, Activation> P1DesignActivation { get; }
    
    /// <summary>
    /// Gets a dictionary of planetary fixing states for each personality planet of person 1.
    /// The value will be calculated on the first call of this property.
    /// </summary>
    public Dictionary<Planets, PlanetaryFixation> P1DesignFixation
    {
        get
        {
            field ??= _planetaryFixations(P1DesignActivation, P1PersonalityActivation, P2PersonalityActivation, P2DesignActivation);
            return field;
        }
    }
    
    /// <summary>
    /// Gets a dictionary of personality activations corresponding to each celestial body for person 2. 
    /// </summary>
    public Dictionary<Planets, Activation> P2PersonalityActivation { get; }
    
    /// <summary>
    /// Gets a dictionary of planetary fixing states for each personality planet of person 2.
    /// The value will be calculated on the first call of this property.
    /// </summary>
    public Dictionary<Planets, PlanetaryFixation> P2PersonalityFixation
    {
        get
        {
            field ??= _planetaryFixations(P2PersonalityActivation, P2DesignActivation, P1PersonalityActivation, P1DesignActivation);
            return field;
        }
    }
    
    /// <summary>
    /// Gets a dictionary of design activations corresponding to each celestial body for person 2. 
    /// </summary>
    public Dictionary<Planets, Activation> P2DesignActivation { get; }
    
    /// <summary>
    /// Gets a dictionary of planetary fixing states for each personality planet of person 2.
    /// The value will be calculated on the first call of this property.
    /// </summary>
    public Dictionary<Planets, PlanetaryFixation> P2DesignFixation
    {
        get
        {
            field ??= _planetaryFixations(P2DesignActivation, P2PersonalityActivation, P1PersonalityActivation, P1DesignActivation);
            return field;
        }
    }

    public Dictionary<Centers, ActivationTypes> CenterActivations
    {
        get
        {
            field ??= HumanDesignUtility.CenterActivations(ConnectedComponents, ChannelActivations);
            return field;
        }
    }

    /// <summary>
    /// Gets a dictionary of connected components, where each center is associated with its components' id.
    /// </summary>
    public Dictionary<Centers, int> ConnectedComponents { get; }
    
    /// <summary>
    /// Gets the number of connected components of the Human Design graph.
    /// </summary>
    public int Splits { get; }

    public SplitDefinitions SplitDefinition => HumanDesignUtility.SplitDefinition(Splits);

    /// <summary>
    /// Gets the set of active gates of the composite chart.
    /// </summary>
    public HashSet<Gates> ActiveGates { get; }
    
    public Dictionary<Gates, ActivationTypes> GateActivations
    {
        get
        {
            field ??= HumanDesignUtility.GateActivations(_p1ActiveGates, _p2ActiveGates);
            return field;
        }
    }
    
    public Dictionary<Channels, ChannelActivationType> ChannelActivations
    {
        get
        {
            field ??= HumanDesignUtility.CompositeChannelActivations(_p1ActiveGates, _p2ActiveGates);
            return field;
        }
    }
    
    #region Constructor
    
    /// <summary>
    /// Initializes a new instance of the <see cref="HumanDesignCompositeChart"/> class using two HumanDesignChart objects.
    /// </summary>
    /// <param name="person1">The first person's Human Design chart.</param>
    /// <param name="person2">The second person's Human Design chart.</param>
    public HumanDesignCompositeChart(HumanDesignChart person1, HumanDesignChart person2)
    {
        P1PersonalityActivation = person1.PersonalityActivation.ToDictionary(x=>x.Key, x=>new Activation
        {
            Gate = x.Value.Gate,
            Line = x.Value.Line,
            Color = x.Value.Color,
            Tone = x.Value.Tone,
            Base = x.Value.Base,
            ColorPercentage = x.Value.ColorPercentage,
            TonePercentage = x.Value.TonePercentage,
            BasePercentage = x.Value.BasePercentage,
            Longitude = x.Value.Longitude
        });
        P1DesignActivation = person1.DesignActivation.ToDictionary(x=>x.Key, x=>new Activation
        {
            Gate = x.Value.Gate,
            Line = x.Value.Line,
            Color = x.Value.Color,
            Tone = x.Value.Tone,
            Base = x.Value.Base,
            ColorPercentage = x.Value.ColorPercentage,
            TonePercentage = x.Value.TonePercentage,
            BasePercentage = x.Value.BasePercentage,
            Longitude = x.Value.Longitude
        });
        P2PersonalityActivation = person2.PersonalityActivation.ToDictionary(x=>x.Key, x=>new Activation
        {
            Gate = x.Value.Gate,
            Line = x.Value.Line,
            Color = x.Value.Color,
            Tone = x.Value.Tone,
            Base = x.Value.Base,
            ColorPercentage = x.Value.ColorPercentage,
            TonePercentage = x.Value.TonePercentage,
            BasePercentage = x.Value.BasePercentage,
            Longitude = x.Value.Longitude
        });
        P2DesignActivation = person2.DesignActivation.ToDictionary(x=>x.Key, x=>new Activation
        {
            Gate = x.Value.Gate,
            Line = x.Value.Line,
            Color = x.Value.Color,
            Tone = x.Value.Tone,
            Base = x.Value.Base,
            ColorPercentage = x.Value.ColorPercentage,
            TonePercentage = x.Value.TonePercentage,
            BasePercentage = x.Value.BasePercentage,
            Longitude = x.Value.Longitude
        });

        _p1ActiveGates = person1.ActiveGates;
        _p2ActiveGates = person2.ActiveGates;
        
        ActiveGates = _p1ActiveGates.ToHashSet();
        ActiveGates.UnionWith(_p2ActiveGates);
        
        (ConnectedComponents, Splits) = GraphService.ConnectedCenters(Utility.HumanDesignUtility.ActiveChannels(ActiveGates));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HumanDesignCompositeChart"/> class using the birthdates of two individuals.
    /// </summary>
    /// <param name="p1DateOfBirth">The date of birth of the first person.</param>
    /// <param name="p2DateOfBirth">The date of birth of the second person.</param>
    /// <param name="eph">The planet position provider used to calculate planetary positions.</param>
    /// <param name="mode">The calculation mode, defaulting to Tropic.</param>
    public HumanDesignCompositeChart(DateTime p1DateOfBirth, DateTime p2DateOfBirth, IPlanetPositionProvider eph, EphCalculationMode mode = EphCalculationMode.Tropic)
    {
        if (p1DateOfBirth.Kind != DateTimeKind.Utc) throw new ArgumentException("The given birthdate 'p1DateOfBirth' is not in UTC");
        if (p2DateOfBirth.Kind != DateTimeKind.Utc) throw new ArgumentException("The given birthdate 'p2DateOfBirth' is not in UTC");
        var p1DesignDate = eph.DesignJulianDay(p1DateOfBirth);
        var p2DesignDate = eph.DesignJulianDay(p2DateOfBirth);
        
        P1PersonalityActivation = Definitions.HumanDesignDefaults.HumanDesignPlanets.ToDictionary(
            p => p,
            p => HumanDesignUtility.ActivationOf(eph.PlanetsPosition(p, p1DateOfBirth, mode).Longitude));
        P1DesignActivation = Definitions.HumanDesignDefaults.HumanDesignPlanets.ToDictionary(
            p => p,
            p => HumanDesignUtility.ActivationOf(eph.PlanetsPosition(p, p1DesignDate, mode).Longitude));
        
        P2PersonalityActivation = Definitions.HumanDesignDefaults.HumanDesignPlanets.ToDictionary(
            p => p,
            p => HumanDesignUtility.ActivationOf(eph.PlanetsPosition(p, p2DateOfBirth, mode).Longitude));
        P2DesignActivation = Definitions.HumanDesignDefaults.HumanDesignPlanets.ToDictionary(
            p => p,
            p => HumanDesignUtility.ActivationOf(eph.PlanetsPosition(p, p2DesignDate, mode).Longitude));
        
        _p1ActiveGates = P1PersonalityActivation.Select(pair => pair.Value.Gate).ToHashSet();
        _p1ActiveGates.UnionWith(P1DesignActivation.Select(pair => pair.Value.Gate).ToHashSet());
        _p2ActiveGates = P2PersonalityActivation.Select(pair => pair.Value.Gate).ToHashSet();
        _p2ActiveGates.UnionWith(P2DesignActivation.Select(pair => pair.Value.Gate).ToHashSet());
        ActiveGates = _p1ActiveGates.ToHashSet();
        ActiveGates.UnionWith(_p2ActiveGates);
        
        (ConnectedComponents, Splits) = GraphService.ConnectedCenters(HumanDesignUtility.ActiveChannels(ActiveGates));
    }

    #endregion
    
    private Dictionary<Planets, PlanetaryFixation> _planetaryFixations(
        Dictionary<Planets, Activation> activations1,
        Dictionary<Planets, Activation> activations2,
        Dictionary<Planets, Activation> comparatorActivations1,
        Dictionary<Planets, Activation> comparatorActivations2)
    {
        return activations1.ToDictionary(p => p.Key,
            p => HumanDesignUtility.CalculateState(p.Key, activations1, activations2, comparatorActivations1, comparatorActivations2));
    }
}