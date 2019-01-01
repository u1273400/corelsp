using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Models
{
    public class Floor: AppBase
    {
        public static Floor[] Floors;
        public static Floor[] CFloors;
        public static long CBId { get; set; } = 2;

        public static object[] flrcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "floorName", name= "Floor ref", field= "floorName", width= 80, minWidth= 80, selectable= false},
            new{id= "gIA", name= "GIA", field= "gIA", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string FloorName { get; set; }
        public double GIA { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        public static Floor[] FromBuilding(){
            CFloors=Floors.Where(c=>c.tableDate==DateTime.Parse(Building.CMonth)&&c.BuildingId==CBId).ToArray();
            return CFloors;
        }

        [JSInvokable]
        public static async Task<String> SetBuilding(long bid){
            CBId=bid;
            await log("set building called "+CBId);
            Init();
            return String.Empty;
        }
        public static async Task<bool> Init(){
            return await JSRuntime.Current.InvokeAsync<bool>("initflrs",FromBuilding(),flrcols);
        }
    }
    
}