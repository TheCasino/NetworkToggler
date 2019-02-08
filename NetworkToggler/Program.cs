using System.Management;

namespace NetworkToggler
{
    internal class Program
    {
        private const string ClassName = "Win32_NetworkAdapter";
        private const string PropName = "NetEnabled";
        private const string DisableMethodName = "Disable";
        private const string EnableMethodName = "Enable";

        private static void Main()
        {
            var management = new ManagementClass(ClassName);
            var instances = management.GetInstances();
            
            foreach (var baseInstance in instances)
            {
                if(!(baseInstance is ManagementObject instance))
                    continue;
                
                var netEnabled = instance[PropName];

                if (!(netEnabled is bool enabled) || !enabled)
                    continue;

                instance.InvokeMethod(DisableMethodName, null);
                instance.InvokeMethod(EnableMethodName, null);
            }
        }
    }
}
