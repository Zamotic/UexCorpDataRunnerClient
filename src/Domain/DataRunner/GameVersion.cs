﻿namespace UexCorpDataRunner.Domain.DataRunner;
public class GameVersion
{
    public const string LiveValue = "Live";
    public const string PtuValue = "Ptu";

    public string Live { get; set; } = string.Empty;
    public string? Ptu { get; set; }
}

