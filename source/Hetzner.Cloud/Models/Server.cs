﻿using Hetzner.Cloud.Enums;

namespace Hetzner.Cloud.Models;

public class Server(long id)
{
    /// <summary>
    /// ID of the Resource
    /// </summary>
    public long Id { get; } = id;
    /// <summary>
    /// Name of the Server (must be unique per Project and a valid hostname as per RFC 1123)
    /// </summary>
    public string Name { get; internal set; } = string.Empty;
    /// <summary>
    /// Status of the Server
    /// </summary>
    public ServerStatus Status { get; internal set; } = ServerStatus.Unknown;
    /// <summary>
    /// Point in time when the Resource was created
    /// </summary>
    public DateTime Created { get; internal set; } = DateTime.MinValue;
    /// <summary>
    /// Inbound Traffic for the current billing period in bytes
    /// </summary>
    public long? IncludedTraffic { get; internal set; }
    /// <summary>
    /// Inbound Traffic for the current billing period in bytes
    /// </summary>
    public long? IngoingTraffic { get; internal set; }
    /// <summary>
    /// Outbound Traffic for the current billing period in bytes
    /// </summary>
    public long? OutgoingTraffic { get; internal set; }
    /// <summary>
    /// True if Server has been locked and is not available to user
    /// </summary>
    public bool Locked { get; internal set; }
    /// <summary>
    /// User-defined labels (key-value pairs)
    /// more informations: https://docs.hetzner.cloud/#labels
    /// </summary>
    public Dictionary<string, string> Labels { get; internal set; } = new();
    /// <summary>
    /// Time window (UTC) in which the backup will run, or null if the backups are not enabled
    /// </summary>
    public string? BackupWindow { get; internal set; }
    /// <summary>
    /// Size of the primary Disk
    /// </summary>
    public long PrimaryDiskSize { get; internal set; }
    /// <summary>
    /// The placement group the server is assigned to.
    /// </summary>
    public PlacementGroup? PlacementGroup { get; internal set; }
    /// <summary>
    /// Datacenter this Resource is located at
    /// </summary>
    public Datacenter? Datacenter { get; internal set; }
    /// <summary>
    /// Protection configuration for the Server
    /// </summary>
    public ServerProtection? Protection { get; internal set; }
    /// <summary>
    /// Image the server is based on.
    /// </summary>
    public Image? Image { get; internal set; }
    /// <summary>
    /// ISO Image that is attached to this Server. Null if no ISO is attached.
    /// </summary>
    public IsoImage? Iso { get; internal set; }
    /// <summary>
    /// True if rescue mode is enabled. Server will then boot into rescue system on next reboot
    /// </summary>
    public bool RescueEnabled { get; internal set; }
    /// <summary>
    /// Load Balancer IDs assigned to the server.
    /// </summary>
    public long[] LoadBalancers { get; internal set; } = Array.Empty<long>();
    /// <summary>
    /// Private networks information
    /// </summary>
    public PrivateNetwork[] PrivateNetworks { get; internal set; } = Array.Empty<PrivateNetwork>();
    /// <summary>
    /// IDs of Volumes assigned to this Server
    /// </summary>
    public long[] Volumes { get; internal set; } = Array.Empty<long>();
    /// <summary>
    /// Private networks information
    /// </summary>
    public ServerType Type { get; internal set; }
}
