using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;
using corelsp.Shared.Models;

namespace corelsp.Shared.Controllers
{
    public class ControllerBase : BlazorComponent
    {
        protected async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }
        protected async Task<bool> init(){
            return await Building.Init();
        }
        protected object[] testdata(){
            return TestData.populate();    
        }
    }
}
    /*
    object[] columns = new object[]{
        new{id= "sel", name= "#", field= "num", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, selectable= false },
        new{id= "title", name= "Title", field= "title", width= 120, minWidth= 120, cssClass= "cell-title", editor= "Slick.Editors.Text", validator= "requiredFieldValidator", sortable= true},
        new{id= "duration", name= "Duration", field= "duration", editor= "Slick.Editors.Text", sortable= true},
        new{id= "%", defaultSortAsc= false, name= "% Complete", field= "percentComplete", width= 80, resizable= false, formatter= "Slick.Formatters.PercentCompleteBar", editor= "Slick.Editors.PercentComplete", sortable= true},
        new{id= "start", name= "Start", field= "start", minWidth= 60, editor= "Slick.Editors.Date", sortable= true},
        new{id= "finish", name= "Finish", field= "finish", minWidth= 60, editor= "Slick.Editors.Date", sortable= true},
        new{id= "effort-driven", name= "Effort Driven", width= 80, minWidth= 20, maxWidth= 80, cssClass= "cell-effort-driven", field= "effortDriven", formatter= "Slick.Formatters.Checkbox", editor= "Slick.Editors.Checkbox", cannotTriggerInsert= true, sortable= true}
    };*/    
