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

        public static Floor[] CFloors;
        public static long CBId { get; set; } = 2;

        public static object[] flrcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, selectable=true, sortable: true },
            new{id= "floorName", name= "Floor ref", field= "floorName", width= 80, minWidth= 80, defaultSortAsc= true, selectable= false, sortable: true},
            new{id= "gia", name= "GIA", field= "gia", minWidth= 100, selectable= false, formatter="Slick.Formatters.Fixed2", sortable: true},
            new{id= "tableDate", name= "tableDate", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string FloorName { get; set; }
        public double Gia { get; set; }
        public DateTime tableDate { get; set; }

        // public static string[] Months(){
        //     return Buildings.OrderBy(c=>c.tableDate).Select(c=>c.tableDate.ToString("yyyy-MM-dd")).Distinct().ToArray();
        // }

        [JSInvokable]
        public static async Task<bool> SetFloors(string data){
            //await log($"Floors::SetFloors: data = {data}");
            CFloors=Json.Deserialize<Floor[]>(data);
            Init();
            return true;
        }

        [JSInvokable]
        public static async Task<bool> SetBuilding(long bid){
            CBId=bid;
            //await log("Floors::SetBuilding: called "+CBId);
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/flr/{CBId}/{Building.CMonth}");
        }

        public static async Task<bool> Init(){
            //if(Space.CFId==null || Space.CFId==0)Space.CFId=CFloors[0].Id;
            Space.CFId=CFloors[0].Id;
            await JSRuntime.Current.InvokeAsync<bool>("initflrs",CFloors,flrcols);
            //log($"Floor::Init: Initialising spaces../api/spr/{Space.CFId}/{Building.CMonth}"); 
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/spr/{Space.CFId}/{Building.CMonth}");
        }
    }
    
}