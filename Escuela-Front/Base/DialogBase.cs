using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Escuela_Front.Base
{
    public class DialogBase : ComponentBase
    {
        [CascadingParameter]
        public IMudDialogInstance? MudDialog { get; set; }

        protected void CloseDialog(bool ok)
        {
            try
            {
                if (MudDialog is null)
                    return;

                if (ok)
                    MudDialog.Close(DialogResult.Ok(true));
                else
                    MudDialog.Cancel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error closing dialog: {ex.Message}");
            }
        }
    }
}
