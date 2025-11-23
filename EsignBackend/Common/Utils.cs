using System;

namespace EsignBackend.Common
{
    public class Utils
    {
        public static string GetEsignPrvKeyValue()
        {
            var comp_var_name = "ESIGN_PRV_KEY";
            return Environment.GetEnvironmentVariable(comp_var_name, EnvironmentVariableTarget.Machine);//Environment.GetEnvironmentVariable(comp_var_name, EnvironmentVariableTarget.User);
        }
    }
}
