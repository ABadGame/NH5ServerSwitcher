using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace NH5ServerSwitcher
{
    public partial class Form1 : Form
    {
        private const string CustomServerUrl = "http://72.39.41.141:8000/";

        public Form1()
        {
            InitializeComponent();

            string defaultSteamPath = @"C:\Program Files (x86)\Steam\steamapps\common\NASCAR Heat 5";
            if (Directory.Exists(defaultSteamPath))
            {
                txtGamePath.Text = defaultSteamPath;
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select your NASCAR Heat 5 Install Directory";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtGamePath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnApplyPatch_Click(object sender, EventArgs e)
        {
            string gameDir = txtGamePath.Text.Trim();
            string managedDir = Path.Combine(gameDir, "NASCARHeat5_Data", "Managed");
            string dllPath = Path.Combine(managedDir, "Assembly-CSharp.dll");
            string backupPath = dllPath + ".bak";

            if (!File.Exists(dllPath))
            {
                MessageBox.Show("Could not find Assembly-CSharp.dll in the selected directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!File.Exists(backupPath))
                {
                    File.Copy(dllPath, backupPath);
                }

                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(managedDir);
                var readerParameters = new ReaderParameters { AssemblyResolver = resolver };

                // Read DLL bytes into memory to release the disk handle immediately
                byte[] assemblyBytes = File.ReadAllBytes(dllPath);

                using (MemoryStream ms = new MemoryStream(assemblyBytes))
                using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(ms, readerParameters))
                {
                    ModuleDefinition module = assembly.MainModule;
                    TypeDefinition ngUtilType = module.Types.FirstOrDefault(t => t.Namespace == "MGI.NG" && t.Name == "NGUtil");

                    if (ngUtilType == null)
                    {
                        MessageBox.Show("Could not locate MGI.NG.NGUtil in Assembly-CSharp.dll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MethodDefinition getBaseUrlMethod = ngUtilType.Methods.FirstOrDefault(m => m.Name == "GetBaseURL");
                    if (getBaseUrlMethod != null)
                    {
                        ILProcessor il = getBaseUrlMethod.Body.GetILProcessor();
                        getBaseUrlMethod.Body.Instructions.Clear();
                        il.Append(il.Create(OpCodes.Ldstr, CustomServerUrl));
                        il.Append(il.Create(OpCodes.Ret));
                    }

                    // Write modified assembly directly back to disk
                    assembly.Write(dllPath);
                }

                lblStatus.Text = "Status: Custom server patch applied!";
                MessageBox.Show("Server patch applied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while patching:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnlockSeasonPass_Click(object sender, EventArgs e)
        {
            string gameDir = txtGamePath.Text.Trim();
            string managedDir = Path.Combine(gameDir, "NASCARHeat5_Data", "Managed");
            string dllPath = Path.Combine(managedDir, "Assembly-CSharp.dll");
            string backupPath = dllPath + ".bak";

            if (!File.Exists(dllPath))
            {
                MessageBox.Show("Could not find Assembly-CSharp.dll in the selected directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (!File.Exists(backupPath))
                {
                    File.Copy(dllPath, backupPath);
                }

                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(managedDir);
                var readerParameters = new ReaderParameters { AssemblyResolver = resolver };

                // Read DLL bytes into memory to release the disk handle immediately
                byte[] assemblyBytes = File.ReadAllBytes(dllPath);

                using (MemoryStream ms = new MemoryStream(assemblyBytes))
                using (AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(ms, readerParameters))
                {
                    ModuleDefinition module = assembly.MainModule;
                    TypeDefinition ngUtilType = module.Types.FirstOrDefault(t => t.Namespace == "MGI.Platform.Steam" && t.Name == "SteamPlatformDLCLoader");

                    if (ngUtilType == null)
                    {
                        MessageBox.Show("Could not locate MGI.NG.NGUtil in Assembly-CSharp.dll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MethodDefinition seasonPassMethod = ngUtilType.Methods.FirstOrDefault(m => m.Name == "do_they_own_the_season_pass");
                    if (seasonPassMethod != null)
                    {
                        ILProcessor il = seasonPassMethod.Body.GetILProcessor();
                        seasonPassMethod.Body.Instructions.Clear();
                        il.Append(il.Create(OpCodes.Ldc_I4_1)); // Returns true
                        il.Append(il.Create(OpCodes.Ret));
                    }

                    // Write modified assembly directly back to disk
                    assembly.Write(dllPath);
                }

                lblStatus.Text = "Status: Season Pass unlocked!";
                MessageBox.Show("Season Pass check bypassed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while unlocking Season Pass:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string gameDir = txtGamePath.Text.Trim();
            string dllPath = Path.Combine(gameDir, "NASCARHeat5_Data", "Managed", "Assembly-CSharp.dll");
            string backupPath = dllPath + ".bak";

            if (File.Exists(backupPath))
            {
                try
                {
                    File.Copy(backupPath, dllPath, true);
                    lblStatus.Text = "Status: Restored official assembly.";
                    MessageBox.Show("Restored original Assembly-CSharp.dll from backup.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to restore backup:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No backup file (.bak) found to restore.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}