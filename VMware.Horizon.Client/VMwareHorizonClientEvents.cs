using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VMware.Horizon.Interop;

namespace VMware.Horizon.Client;

public class VMwareHorizonClientEvents : IVMwareHorizonClientEvents5
{
    private readonly Action<int, string> _callbackMessage;

    public VMwareHorizonClientEvents(Action<int, string> callbackMessage)
    {
        _callbackMessage = callbackMessage;
    }

    public void OnStarted()
    {
        DispatchMessage(3, "Started Called");
    }

    public void OnExit()
    {
        DispatchMessage(3, "Exit Called");
    }

    public void OnConnecting(object serverInfo)
    {
        var info = (IVMwareHorizonClientServerInfo)serverInfo;
        DispatchMessage(3, string.Format("Connecting, Server Address: {0}, ID: {1}, Type:{2} ",
            info.serverAddress, info.serverId, info.serverType.ToString()));
    }

    public void OnConnectFailed(uint serverId, string errorMessage)
    {
        DispatchMessage(3, $"Connect Failed, Server ID: {serverId}, Message: {errorMessage}");
    }

    public void OnAuthenticationRequested(uint serverId, VmwHorizonClientAuthType authType)
    {
        DispatchMessage(3, $"Authentication Requested, Server ID: {serverId}, AuthType: {authType.ToString()}");
    }

    public void OnAuthenticating(uint serverId, VmwHorizonClientAuthType authType, string user)
    {
        DispatchMessage(3, $"Authenticating, Server ID: {serverId}, AuthType: {authType.ToString()}, User: {user}");
    }

    public void OnAuthenticationDeclined(uint serverId, VmwHorizonClientAuthType authType)
    {
        DispatchMessage(3, string.Format("Authentication Declined, Server ID: {0}, AuthType: {1}",
            serverId, authType.ToString()));
    }

    public void OnAuthenticationFailed(uint serverId, VmwHorizonClientAuthType authType, string errorMessage,
        int retryAllowed)
    {
        DispatchMessage(3,
            $"Authentication Failed, Server ID: {serverId}, AuthType: {authType.ToString()}, Error: {errorMessage}, retry allowed?: {retryAllowed}");
    }

    public void OnLoggedIn(uint serverId)
    {
        DispatchMessage(3, $"Logged In, Server ID: {serverId}");
    }

    public void OnDisconnected(uint serverId)
    {
        DispatchMessage(3, $"Disconnected, Server ID: {serverId}");
    }

    public void OnReceivedLaunchItems(uint serverId, Array launchItems)
    {
        DispatchMessage(3, $"Received Launch Items, Server ID: {serverId}, Item Count: {launchItems.Length}");
        var items = Helpers.GetLaunchItems(launchItems);
        foreach (var item in items)
        {
            DispatchMessage(3,
                $"Launch Item: Server ID: {serverId}, Name: {item.Name}, Type: {item.Type.ToString()}, ID: {item.Id}");
        }
    }

    public void OnLaunchingItem(uint serverId, VmwHorizonLaunchItemType type, string launchItemId,
        VmwHorizonClientProtocol protocol)
    {
        DispatchMessage(3,
            $"Launching Item, Server ID: {serverId}, type: {type.ToString()}, Item ID: {launchItemId}, Protocol: {protocol.ToString()}");
    }

    public void OnItemLaunchSucceeded(uint serverId, VmwHorizonLaunchItemType type, string launchItemId)
    {
        DispatchMessage(3,
            string.Format("Launch Item Succeeded, Server ID: {0}, Type: {1}, ID: {2}", serverId,
                type.ToString(), launchItemId));
    }

    public void OnItemLaunchFailed(uint serverId, VmwHorizonLaunchItemType type, string launchItemId,
        string errorMessage)
    {
        DispatchMessage(3, string.Format("Launch Item Succeeded, Server ID: {0}, type: {1}, Item ID: {2}",
            serverId,
            type.ToString(), launchItemId));
    }

    public void OnNewProtocolSessionCreated(uint serverId, string sessionToken,
        VmwHorizonClientProtocol protocol, VmwHorizonClientSessionType type, string clientId)
    {
        DispatchMessage(3,
            string.Format(
                "New Protocol Session Created, Server ID: {0}, Token: {1}, Protocol: {2}, Type: {3}, ClientID: {4}",
                serverId, sessionToken, protocol.ToString(), type.ToString(), clientId));
    }

    public void OnProtocolSessionDisconnected(uint serverId, string sessionToken, uint connectionFailed,
        string errorMessage)
    {
        DispatchMessage(3, string.Format("" +
                                         "Protocol Session Disconnected, Server ID: {0}, Token: {1}, ConnectFailed: {2}, Error: {3}",
            serverId, sessionToken, connectionFailed, errorMessage));
    }

    public void OnSeamlessWindowsModeChanged(uint serverId, string sessionToken, uint enabled)
    {
        DispatchMessage(3,
            string.Format("Seamless Window Mode Changed, Server ID: {0}, Token: {1}, Enabled: {2}",
                serverId, sessionToken, enabled));
    }

    public void OnSeamlessWindowAdded(uint serverId, string sessionToken, string windowPath,
        string entitlementId, int windowId, long windowHandle, VmwHorizonClientSeamlessWindowType type)
    {
        DispatchMessage(3, string.Format(
            "Seamless Window Added, Server ID: {0}, Token: {1}, WindowPath: {2}, EntitlementID: {3}, WindowID: {4}, WindowHandle: {5}, Type: {6}",
            serverId, sessionToken, windowPath, entitlementId, windowId, windowHandle, type.ToString()));
    }

    public void OnSeamlessWindowRemoved(uint serverId, string sessionToken, int windowId)
    {
        DispatchMessage(3, string.Format(
            "Seamless Window Removed, Server ID: {0}, Token: {1}, WindowID: {2}",
            serverId, sessionToken, windowId));
    }

    public void OnUSBInitializeComplete(uint serverId, string sessionToken)
    {
        DispatchMessage(3, string.Format(
            "USB Initialize Complete, Server ID: {0}, Token: {1}",
            serverId, sessionToken));
    }

    public void OnConnectUSBDeviceComplete(uint serverId, string sessionToken, uint isConnected)
    {
        DispatchMessage(3, string.Format(
            "Connect USB Device Complete, Server ID: {0}, Token: {1}, IsConnected: {2}",
            serverId, sessionToken, isConnected));
    }

    public void OnUSBDeviceError(uint serverId, string sessionToken, string errorMessage)
    {
        DispatchMessage(3, string.Format(
            "Connect USB Device Error, Server ID: {0}, Token: {1}, Error: {2}",
            serverId, sessionToken, errorMessage));
    }

    public void OnAddSharedFolderComplete(uint serverId, string fullPath, uint succeeded, string errorMessage)
    {
        DispatchMessage(3, string.Format(
            "Add Shared Folder Complete, Server ID: {0}, FullPath: {1}, Succeeded: {2}, Error: {3}",
            serverId, fullPath, succeeded, errorMessage));
    }

    public void OnRemoveSharedFolderComplete(uint serverId, string fullPath, uint succeeded,
        string errorMessage)
    {
        DispatchMessage(3, string.Format(
            "Remove Shared Folder Complete, Server ID: {0}, FullPath: {1}, Succeeded: {2}, Error: {3}",
            serverId, fullPath, succeeded, errorMessage));
    }

    public void OnFolderCanBeShared(uint serverId, string sessionToken, uint canShare)
    {
        DispatchMessage(3, string.Format(
            "Folder Can Be Shared, Server ID: {0}, Token: {1}, canShare: {2}",
            serverId, sessionToken, canShare));
    }

    public void OnCDRForcedByAgent(uint serverId, string sessionToken, uint forcedByAgent)
    {
        DispatchMessage(3, string.Format(
            "CDR Forced By Agent, Server ID: {0}, Token: {1}, Forced: {2}",
            serverId, sessionToken, forcedByAgent));
    }

    public void OnItemLaunchSucceeded2(uint serverId, VmwHorizonLaunchItemType type, string launchItemId,
        string sessionToken)
    {
        DispatchMessage(3,
            string.Format("Item Launch Succeeded(2), Server ID: {0}, Type: {1}, ID: {2}, token: {3}", serverId,
                type.ToString(), launchItemId, sessionToken));
    }

    public void OnReceivedLaunchItems2(uint serverId, Array launchItems)
    {
        DispatchMessage(3,
            string.Format("Received Launch Items2, Server ID: {0}, Item Count: {1}", serverId,
                launchItems.Length));
        var Items = Helpers.GetLaunchItems2(launchItems);
        foreach (var item in Items)
        {
            DispatchMessage(3,
                string.Format("Launch Item: Server ID: {0}, Name: {1}, Type: {2}, ID: {3}, Remotable: {4}",
                    serverId, item.Name, item.Type.ToString(), item.Id, item.HasRemotableAssets));
        }
    }

    private void DispatchMessage(int severity, string message)
    {
        _callbackMessage.Invoke(severity, message);
    }

    private string SerialiseObject(object value) =>
        JsonConvert.SerializeObject(value, Formatting.Indented);

    public class Helpers
    {
        [Flags]
        public enum LaunchItemType
        {
            VmwHorizonLaunchItem_HorizonDesktop = 0,
            VmwHorizonLaunchItem_HorizonApp = 1,
            VmwHorizonLaunchItem_XenApp = 2,
            VmwHorizonLaunchItem_SaaSApp = 3,
            VmwHorizonLaunchItem_HorizonAppSession = 4,
            VmwHorizonLaunchItem_DesktopShadowSession = 5,
            VmwHorizonLaunchItem_AppShadowSession = 6
        }

        [Flags]
        public enum SupportedProtocols
        {
            VmwHorizonClientProtocol_Default = 0,
            VmwHorizonClientProtocol_RDP = 1,
            VmwHorizonClientProtocol_PCoIP = 2,
            VmwHorizonClientProtocol_Blast = 4
        }


        public static List<LaunchItem> GetLaunchItems(Array ItemList)
        {
            var returnList = new List<LaunchItem>();
            foreach (var item in ItemList)
            {
                returnList.Add(new LaunchItem((IVMwareHorizonClientLaunchItemInfo)item));
            }

            return returnList;
        }

        public static List<LaunchItem2> GetLaunchItems2(Array ItemList)
        {
            var returnList = new List<LaunchItem2>();
            foreach (var item in ItemList)
            {
                returnList.Add(new LaunchItem2((IVMwareHorizonClientLaunchItemInfo2)item));
            }

            return returnList;
        }

        public class LaunchItem
        {
            public LaunchItem(IVMwareHorizonClientLaunchItemInfo item)
            {
                Name = item.name;
                Id = item.id;
                Type = (LaunchItemType)item.type;
                SupportedProtocols = (SupportedProtocols)item.supportedProtocols;
                DefaultProtocol = item.defaultProtocol;
            }

            public string Name { get; set; }

            public string Id { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public LaunchItemType Type { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public SupportedProtocols SupportedProtocols { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public VmwHorizonClientProtocol DefaultProtocol { get; set; }
        }

        public class LaunchItem2
        {
            public LaunchItem2(IVMwareHorizonClientLaunchItemInfo2 i)
            {
                Name = i.name;
                Id = i.id;
                Type = (LaunchItemType)i.type;
                SupportedProtocols = (SupportedProtocols)i.supportedProtocols;
                DefaultProtocol = i.defaultProtocol;
                HasRemotableAssets = i.hasRemotableAssets;
            }

            public string Name { get; set; }

            public string Id { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public LaunchItemType Type { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public SupportedProtocols SupportedProtocols { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public VmwHorizonClientProtocol DefaultProtocol { get; set; }

            public uint HasRemotableAssets { get; set; }
        }
    }
}