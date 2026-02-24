namespace Customer_Service.Helpers
{
    public class Masking

    {
        public static string MaskAadhaar(string aadhaar)
        {
            if (aadhaar.Length < 4)
                return "XXXX";

            return "XXXX-XXXX-" + aadhaar[^4..];
        }

        public static string MaskPan(string pan)
        {
            if (string.IsNullOrEmpty(pan) || pan.Length < 4)
                return "XXXX";

            return pan.Substring(0, 2) + "XXXXX" + pan[^1];
        }

    }
}
