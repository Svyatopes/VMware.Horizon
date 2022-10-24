namespace VMware.Horizon.Interop
{
    public enum VmwHorizonClientAuthType
    {
        VmwHorizonClientAuthType_Unknown,
        VmwHorizonClientAuthType_Disclaimer,
        VmwHorizonClientAuthType_SecurID_Passcode,
        VmwHorizonClientAuthType_SecurID_NextTokencode,
        VmwHorizonClientAuthType_SecurID_PIN_Change,
        VmwHorizonClientAuthType_SecurID_Wait,
        VmwHorizonClientAuthType_Windows_Password,
        VmwHorizonClientAuthType_Windows_Password_Expired,
        VmwHorizonClientAuthType_Certificate,
        VmwHorizonClientAuthType_SAML,
        VmwHorizonClientAuthType_GSSAPI,
        VmwHorizonClientAuthType_Unauthenticated_Access
    }
}