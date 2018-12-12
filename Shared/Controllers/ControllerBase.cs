using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Blazor.Components;

namespace corelsp.Shared.Controllers
{
    public class ControllerBase : BlazorComponent
    {
        protected async Task<bool> log(object msg){
            return await JSRuntime.Current.InvokeAsync<bool>("log",msg);
        }
        protected async Task<bool> init(object[] data,object[] cols){
                return await JSRuntime.Current.InvokeAsync<bool>("init",data,cols);
        }
        protected TestData[] poptestdata(){
            TestData[] d = new TestData[50000];
            for (var i = 0; i < d.Length; i++) {
                d[i]=new TestData();
                d[i].id = "id_" + i;
                d[i].num = i;
                d[i].title = "Task " + i;
                d[i].duration = "5 days";
                d[i].percentComplete = (new Random()).Next(1,100);
                d[i].start = "01/01/2009";
                d[i].finish = "01/05/2009";
                d[i].effortDriven = (i % 5 == 0);
                if((i+1) % 25000==0)log($"populating ..{(i+1)*1.0/d.Length*100}% complete");
            }
            return d;   
        }
    }
    public class TestData{
        public string id;
        public int num;
        public string title;
        public string duration;
        public int percentComplete;
        public string start;
        public string finish;
        public bool effortDriven;
    }
}