#pragma checksum "H:\Downloads\src\core\lsp\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f6ce12f8cac2bf6fbb628dc569406a598f8829bf"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace lsp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using lsp;
    using lsp.Shared;
    [Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/")]
    public class Index : Microsoft.AspNetCore.Blazor.Components.BlazorComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder builder)
        {
        }
        #pragma warning restore 1998
#line 21 "H:\Downloads\src\core\lsp\Pages\Index.cshtml"
            
    int currentCount = 0;

    void IncrementCount()
    {
        currentCount++; 
    }
    protected override async Task OnInitAsync()
    {
        // Simulate async initialization work
        await Task.Delay(1000);
        await JSRuntime.Current.InvokeAsync<bool>("init");

    }
    protected override void OnInit()
    {
        // Simulate async initialization work
        //await Task.Delay(1000);
        JSRuntime.Current.InvokeAsync<bool>("log",new {msg="Init called"});


    }



#line default
#line hidden
    }
}
#pragma warning restore 1591
