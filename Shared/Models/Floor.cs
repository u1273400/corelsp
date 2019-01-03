using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Http=Microsoft.AspNetCore.Components.HttpClientJsonExtensions;
using System.Net.Http;

namespace corelsp.Shared.Models
{
    public class Floor: AppBase
    {
        //public static Floor[] Floors;
        public static Floor[] CFloors;
        public static long CBId { get; set; } = 2;

        public static object[] flrcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, defaultSortAsc= true, selectable=true },
            new{id= "floorName", name= "Floor ref", field= "floorName", width= 80, minWidth= 80, selectable= false},
            new{id= "gia", name= "GIA", field= "gia", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string FloorName { get; set; }
        public double Gia { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        public static async Task<Floor[]> FromBuilding(){
            return await Http.GetJsonAsync<Floor[]>(new HttpClient(), $"../api/flr/{CBId}/{Building.CMonth}");// as Floor[];
            //CFloors=Floors.Where(c=>c.tableDate==DateTime.Parse(Building.CMonth)&&c.BuildingId==CBId).ToArray();
        }

        [JSInvokable]
        public static async Task<String> SetBuilding(long bid){
            CBId=bid;
            await log("set building called "+CBId);
            Init();
            return String.Empty;
        }
        public static async Task<bool> Init(){
            CFloors=FromBuilding().Result;
            return await JSRuntime.Current.InvokeAsync<bool>("initflrs",CFloors,flrcols);
        }
    }
    
}