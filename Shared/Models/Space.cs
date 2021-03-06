using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Http=corelsp.Shared.Helpers.HttpClientJson2;
//using Microsoft.AspNetCore.Blazor;
//using Http=System.Net.Http.HttpClient;HttpClientJson2

namespace corelsp.Shared.Models
{
    public class Space: AppBase
    {

        public static Space[] CSpaces;
        public static Space CSpace;
        public static long CFId { get; set; }

        public static object[] spcols = new object[]{
            new{id= "id", name= "Id", field= "id", behavior= "select", cssClass= "cell-selection", width= 40, cannotTriggerInsert= true, resizable= false, sortable= true, selectable=true },
            new{id= "label", name= "Space", field= "label", width= 80, minWidth= 80, defaultSortAsc= true, selectable= false, sortable=true, editor= "AutoCompleteEditor", dataSource= "DataSource.Labels"},
            new{id= "area", name= "Area", field= "area", minWidth= 40, selectable= false, sortable= true, editor= "Slick.Editors.Text"},
            new{id= "usageName", name= "Usage", field= "usageName", minWidth= 200, selectable= false, sortable= true},
            new{id= "capacity", name= "Capacity", field= "capacity", minWidth= 30, selectable= false, sortable=true, editor= "Slick.Editors.Text"},
            new{id= "dept", name= "Department", field= "dept", minWidth= 175, selectable= false, sortable= true, editor= "AutoCompleteEditor", dataSource= "DataSource.Departments"},
            new{id= "asasName", name= "Asas", field= "asasName", minWidth= 175, selectable= false, sortable= true},
            new{id= "tableDate", name= "tableDate", field= "tableDate", minWidth= 100, selectable= false}
        };

        public long Id { get; set; }
        public string Label { get; set; }
        public string Dept { get; set; }
        public long DeptId { get; set; }
        public double Area { get; set; }
        public string UsageName { get; set; }
        public string AsasName { get; set; }
        public long AsasId { get; set; }
        public long SpaceTransactionId { get; set; }
        public long Capacity { get; set; }
        public DateTime tableDate { get; set; }
        //public string _token { get; set; }="testenv";
        public Space(){}

        [JSInvokable]
        public static async Task<bool> SetSpaces(string data){
            //await log($"Floors::SetFloors: data = {data}");
            CSpaces=Json.Deserialize<Space[]>(data);
            Init();
            return true;
        }

        [JSInvokable]
        public static async Task<bool> SetSpace(string data){
            //await log($"Floors::SetFloors: data = {data}");
            CSpace=Json.Deserialize<Space>(data);
            var spc=new{
                SpaceId = CSpace.Id,
                SpaceLabel = CSpace.Label.ToUpper(),
                DepartmentId = Params.InitData.DeptsMenu.Where(c => c.Value==CSpace.Dept).Single().Key,
                UsageId = Params.InitData.UsagesMenu.Where(c => c.Value==CSpace.UsageName).Single().Key,
                //AsasId = Params.InitData.AsasList.Where(c => c.Value==CSpace.AsasName).Single().Key,
                Area = CSpace.Area,
                SpaceTransactionId = CSpace.SpaceTransactionId,
                Capacity = CSpace.Capacity,
                TransactionDate=DateTime.Now.ToString("yyyy-MM-dd H:m:s"),
                _token ="testenv"
            };
            log("save obj: "+spc);
            var result=await Http.PostJsonAsync<AppJsonResponse>("http://iris-dev.hud.ac.uk:8000/api/updateSpaceTx",spc);
            log("save result="+result);
            return result.status=="success";
        }

        [JSInvokable]
        public static async Task<bool> SetFloor(long fid){
            CFId=fid;
            log($"set floor called {CFId}/{Building.CMonth}");
            return await JSRuntime.Current.InvokeAsync<bool>("initFloors",$"../api/spr/{Space.CFId}/{Building.CMonth}");
        }
        public static async Task<bool> Init(){
            //await log($"Space::Init: Cspaces={CSpaces.Length}");
            return await JSRuntime.Current.InvokeAsync<bool>("initspaces",CSpaces,spcols);
        }
    }
    public class SpaceSaveObj: AppBase
    {
        public long SpaceId { get; set; }
        public string SpaceLabel  { get; set; }
        public long DepartmentId  { get; set; }
        public long UsageId  { get; set; }
        public long AsasId  { get; set; }
        public double Area  { get; set; }
        public long Capacity  { get; set; }
        public string Cmd  { get; set; }
        public string TransactionDate  { get; set; }
        public SpaceSaveObj(){}

    }
}
