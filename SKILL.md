---
name: Human Design Chart Reader
description: |
  Calculate and interpret Human Design body graphs from birth data —
  type, profile, authority, incarnation cross, channels, gates, and variables.
---

# Human Design Chart Reader

Provenance: shaped from [SharpAstrology.HumanDesign](https://github.com/CReizner/SharpAstrology.HumanDesign),
a C# library for computing Human Design body graphs. The library itself is a
calculation engine; this skill wraps its domain knowledge into an interactive
reading workflow.

Source URL: https://github.com/CReizner/SharpAstrology.HumanDesign

## What This Skill Does

Given a person's birth date, time, and location (UTC), produce a complete Human
Design chart reading covering:

1. **Type** — Manifestor, Generator, Manifesting Generator, Projector, or
   Reflector.
2. **Profile** — the two-number archetype (e.g. 1/3 Investigator/Martyr).
3. **Authority** — the decision-making strategy (Emotional/Solar Plexus,
   Sacral, Splenic, Ego/Heart, Self-Projected, Mental/None, Lunar).
4. **Split Definition** — Single, Split, Triple Split, Quadruple Split, or No
   Definition.
5. **Incarnation Cross** — the life purpose cross derived from Sun and Earth
   gate positions across personality and design.
6. **Active Channels and Gates** — which of the 64 gates and 36 channels are
   activated, listing both personality (conscious) and design (unconscious)
   sides.
7. **Variables** — Digestion, Perspective, Environment, and Awareness with
   their color, tone, and orientation (Left/Right arrow).
8. **Planetary Activations** — gate and line for each planet on both
   personality and design sides, with fixation states (exalted, detriment,
   juxtaposed).

## Additional Reading Modes

- **Composite Chart** — given two birth datetimes, analyze the relationship
  dynamics: which channels are magnetic, dominated by one person, or
  compromised. Explain the interplay between the two body graphs.
- **Transit Chart** — given a birth datetime and a transit datetime, show which
  channels are activated by current planetary positions and what themes they
  bring.
- **Birth Time Uncertainty** — when the user doesn't know their exact birth
  time, explore a time range and report which chart variations appear and their
  approximate probability.

## Inputs

- **birth_datetime** — date and time of birth in UTC (or with timezone offset).
  Required.
- **birth_datetime_2** — second person's birth data, for composite readings.
  Optional.
- **transit_datetime** — point in time for transit analysis. Optional.
- **time_range** — start and end times when exact birth time is unknown.
  Optional.

## Output Contract

Return a structured reading that includes:

- All chart components listed above, clearly labeled.
- A plain-language interpretation of what each component means for the person.
- For composite charts: relationship dynamics, electromagnetic connections,
  dominance patterns, and compromise channels.
- For transits: which transiting planets activate which gates/channels and the
  themes in play.

## Interpretation Guidelines

- Explain Human Design terminology in accessible language — the user may be new
  to the system.
- Distinguish between personality (conscious/red) and design (unconscious/black)
  activations.
- Note when planets are in exalted or detriment positions and what that implies.
- For Variables, explain the practical meaning of each arrow direction
  (Left = strategic/focused, Right = receptive/peripheral).
- When the user provides a time range instead of exact time, present all
  possible chart variations with their probability windows.

## Calculation Reference

This skill's domain model follows the calculation framework of the
SharpAstrology.HumanDesign library:

- Gate positions derived from planetary longitudes mapped to the 64-gate wheel
  (I Ching hexagram order on the zodiac).
- Lines determined by subdividing each gate's 5.625° arc into 6 lines of
  56.25 arcminutes each.
- Variables (color, tone, base) computed from finer subdivisions: color =
  9.375 arcminutes, tone = 93.75 arcseconds, base = 15.625 arcseconds.
- Design calculation uses the Sun's position 88° before birth to determine the
  design crystal activation moment.
- Incarnation cross derived from the Sun and Earth gates on both personality
  and design sides.

Key source paths in the original library:
- Chart construction: [DataModels/HumanDesignChart.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/DataModels/HumanDesignChart.cs)
- Composite charts: [DataModels/HumanDesignCompositeChart.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/DataModels/HumanDesignCompositeChart.cs)
- Transit charts: [DataModels/HumanDesignTransitChart.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/DataModels/HumanDesignTransitChart.cs)
- Variables model: [DataModels/Variables.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/DataModels/Variables.cs)
- Gate definitions: [Enums/Gates.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/Enums/Gates.cs)
- Channel definitions: [Enums/Channels.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/Enums/Channels.cs)
- Type classification: [Enums/Types.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/Enums/Types.cs)
- Incarnation crosses: [Enums/IncarnationCross.cs](https://github.com/CReizner/SharpAstrology.HumanDesign/blob/master/Enums/IncarnationCross.cs)
