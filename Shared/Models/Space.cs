using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Space: AppBase
    {
        public static Space[] Spaces;
        public static long CFId { get; set; } 

        public static object[] spcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "label", name= "Space", field= "label", width= 80, minWidth= 80, selectable= false},
            new{id= "dept", name= "Department", field= "dept", minWidth= 150, selectable= false},
            new{id= "area", name= "Area", field= "area", minWidth= 100, selectable= false},
            new{id= "tableDate", name= "tableDate", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long Label { get; set; }
        public string Dept { get; set; }
        public long DeptId { get; set; }
        public string Area { get; set; }
        public long Floor { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        public static async Task<Space[]> FromFloor(){
            await log($" Spaces::FromFloor: CFId={CFId}");
            return Spaces.Where(c=>c.tableDate==DateTime.Parse(Building.CMonth)&&c.Floor==CFId).ToArray();
        }

        [JSInvokable]
        public static async Task<String> SetFloor(long fid){
            CFId=fid;
            await log("set building called "+CFId);
            Init();
            return String.Empty;
        }
        public static async Task<bool> Init(){
            return await JSRuntime.Current.InvokeAsync<bool>("initspaces",FromFloor(),spcols);
        }
    }
    
}