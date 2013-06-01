using System.Windows.Forms;

namespace FileIntegrityMonitor.Configurator
{
    public static class FormsHelpers
    {
        public static bool ToBoolean(this DialogResult dialogResult)
        {
            return dialogResult == DialogResult.Yes || dialogResult == DialogResult.OK;
        }
    }
}