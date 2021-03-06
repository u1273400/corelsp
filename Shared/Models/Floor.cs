using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Http=corelsp.Shared.Helpers.HttpClientJson2;
using System.Net.Http;

namespace corelsp.Shared.Models
{
    public class Floor: AppBase
    {

        public static Floor[] CFloors;
        public static long CBId { get; set; } = 2;

        public static object[] flrcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, selectable=true, sortable= true },
            new{id= "floorName", name= "Floor ref", field= "floorName", width= 80, minWidth= 80, defaultSortAsc= true, selectable= false, sortable= true},
            new{id= "gia", name= "GIA", field= "gia", minWidth= 100, selectable= false, formatter="Slick.Formatters.Fixed2", sortable= true, editor= "Slick.Editors.Text"},
            new{id= "tableDate", name= "tableDate", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string FloorName { get; set; }
        public double Gia { get; set; }
        public DateTime tableDate { get; set; }

        public Floor(){}

        [JSInvokable]
        public static async Task<bool> SetFloors(string data){
            // log($"Floors::SetFloors: data = {data}");
            CFloors=Json.Deserialize<Floor[]>(data);
            Init();
            return true;
        }

        [JSInvokable]
        public static async Task<bool> SetBuilding(long bid){
            CBId=bid;
            log("Floors::SetBuilding: called "+CBId);
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/flr/{CBId}/{Building.CMonth}");
        }

        [JSInvokable]
        public static async Task<bool> SaveFloorGia(string data){
            var CFloor=Json.Deserialize<Floor>(data);
            var flr=new{
                FloorId = CFloor.Id,
                TransactionDate=DateTime.Now.ToString("yyyy-MM-dd H:m:s"),
                GrossInternalArea = CFloor.Gia,
                _token ="testenv",
            };
            log("save obj: "+flr);
            var result=await Http.PostJsonAsync<FloorJsonResponse>("http://iris-dev.hud.ac.uk:8000/api/updateFloorTx",flr);
            log("save result="+result);
            return result.status=="success";
        }

        public static async Task<bool> Init(){
            //if(Space.CFId==null || Space.CFId==0)Space.CFId=CFloors[0].Id;
            if(CFloors.Count()>0)
                Space.CFId=CFloors[0].Id;
            else
                Space.CFId=0;
            // log($"Floor::Init: Initialising floors");
            await JSRuntime.Current.InvokeAsync<bool>("initflrs",CFloors,flrcols);
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/spr/{Space.CFId}/{Building.CMonth}");
        }
    }
    public class FloorSaveObj: AppBase
    {
        public string    Cmd{ get; set; }
        public string    FloorBuildingRef{ get; set; }
        public long    FloorId{ get; set; }
        public string    FloorLiveDate{ get; set; }
        public long    FloorTransactionId{ get; set; }
        public double    GrossInternalArea{ get; set; }
        // public long    ModifiedBy{ get; set; }
        public string    Notes{ get; set; }
        public string    TransactionDate{ get; set; }
        public FloorSaveObj(){}
    }
}
