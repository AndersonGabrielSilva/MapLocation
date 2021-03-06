using MapLocation.Client.Extensions;
using MapLocationShared.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapLocation.Client.Shared.Component.GeographicLocation
{
    public class LayoutDialogBase : ComponentBase
    {
        #region Parameter
        [CascadingParameter]
        protected MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string TypeMapsDialog { get; set; }
        #endregion

        #region Properties
        public List<LayoutList> ListaLayout { get; set; }
        #endregion

        #region Constructor
        public LayoutDialogBase()
        {
            ListaLayout = new List<LayoutList>();
            ListaLayout.CreateLayoutList();
        }
        #endregion

        #region Events
        protected void Cancel() =>
            MudDialog.Cancel();

        protected void Submit() 
        {
            Console.WriteLine(TypeMapsDialog);
            MudDialog.Close(DialogResult.Ok(TypeMapsDialog));
        }
        #endregion
    }
}
