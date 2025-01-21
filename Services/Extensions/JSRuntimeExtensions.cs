using Microsoft.JSInterop;

namespace YumBlazor.Services.Extensions;

public static class JSRuntimeExtensions
{
    public static async Task ToastrSuccess(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }
    
    public static async Task ToastrError(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }
}